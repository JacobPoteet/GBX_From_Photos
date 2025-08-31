using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace GBX_From_Photos
{
    public class PhotoProcessor
    {
        public async Task<ProcessingResult> ProcessPhotosAsync(string folderPath, List<string> supportedExtensions, string outputFolder, IProgress<ProcessingProgress> progress)
        {
            var result = new ProcessingResult();
            var logFilePath = Path.Combine(outputFolder, "errors.log");
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var gpxFilePath = Path.Combine(outputFolder, $"photos_export{timestamp}.gpx");
            
            result.LogFilePath = logFilePath;
            result.GpxFilePath = gpxFilePath;

            try
            {
                // Get all photos recursively
                var allPhotos = GetPhotosRecursively(folderPath, supportedExtensions);
                result.TotalPhotos = allPhotos.Count;

                if (result.TotalPhotos == 0)
                {
                    // Create empty GPX file
                    CreateEmptyGpxFile(gpxFilePath);
                    return result;
                }

                var waypoints = new List<GpsWaypoint>();
                var processedCount = 0;
                var successfulCount = 0;
                var skippedCount = 0;
                var errorCount = 0;

                // Process each photo
                foreach (var photoPath in allPhotos)
                {
                    try
                    {
                        var waypoint = ExtractGpsData(photoPath);
                        
                        if (waypoint != null)
                        {
                            // Add original waypoint
                            waypoints.Add(waypoint);

                            // Add offset waypoint (very close)
                            waypoints.Add(new GpsWaypoint
                            {
                                Latitude = waypoint.Latitude + 0.00001, // ~1.1 meters north
                                Longitude = waypoint.Longitude,
                                Name = waypoint.Name + " (offset)"
                            });

                            successfulCount++;
                        }
                        else
                        {
                            skippedCount++;
                            LogError(logFilePath, photoPath, "No GPS data found");
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        LogError(logFilePath, photoPath, ex.Message);
                    }

                    processedCount++;
                    
                    // Update progress
                    var progressData = new ProcessingProgress
                    {
                        TotalPhotos = result.TotalPhotos,
                        ProcessedPhotos = processedCount,
                        RemainingPhotos = result.TotalPhotos - processedCount,
                        SuccessfulPhotos = successfulCount,
                        SkippedPhotos = skippedCount,
                        ErrorPhotos = errorCount,
                        Percentage = (int)((double)processedCount / result.TotalPhotos * 100),
                        SuccessRate = result.TotalPhotos > 0 ? (double)successfulCount / result.TotalPhotos * 100 : 0
                    };
                    
                    progress?.Report(progressData);
                }

                // Generate GPX file
                await GenerateGpxFileAsync(gpxFilePath, waypoints);

                // Update final result
                result.SuccessfulPhotos = successfulCount;
                result.SkippedPhotos = skippedCount;
                result.ErrorPhotos = errorCount;
                result.SuccessRate = result.TotalPhotos > 0 ? (double)successfulCount / result.TotalPhotos * 100 : 0;

                return result;
            }
            catch (Exception ex)
            {
                LogError(logFilePath, "PROCESSING", ex.Message);
                throw;
            }
        }

        private List<string> GetPhotosRecursively(string folderPath, List<string> supportedExtensions)
        {
            var photos = new List<string>();
            
            try
            {
                var files = System.IO.Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file).ToLowerInvariant();
                    if (supportedExtensions.Contains(extension))
                    {
                        photos.Add(file);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error scanning folder: {ex.Message}");
            }

            return photos;
        }

        private GpsWaypoint? ExtractGpsData(string photoPath)
        {
            try
            {
                var directories = ImageMetadataReader.ReadMetadata(photoPath);
                var gpsDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();

                if (gpsDirectory != null)
                {
                    var latitude = gpsDirectory.GetGeoLocation()?.Latitude;
                    var longitude = gpsDirectory.GetGeoLocation()?.Longitude;

                    if (latitude.HasValue && longitude.HasValue)
                    {
                        return new GpsWaypoint
                        {
                            Latitude = latitude.Value,
                            Longitude = longitude.Value,
                            Name = Path.GetFileName(photoPath)
                        };
                    }
                }

                return null;
            }
            catch (Exception)
            {
                // Return null for any errors - they will be logged as errors
                return null;
            }
        }

        private async Task GenerateGpxFileAsync(string filePath, List<GpsWaypoint> waypoints)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                Encoding = System.Text.Encoding.UTF8,
                Async = true
            };

            try
            {
                using var writer = XmlWriter.Create(filePath, settings);

                // Write GPX header
                await writer.WriteStartDocumentAsync();
                await writer.WriteStartElementAsync(null, "gpx", "http://www.topografix.com/GPX/1/1");
                await writer.WriteAttributeStringAsync(null, "version", null, "1.1");
                await writer.WriteAttributeStringAsync(null, "creator", null, "PhotoToGPX");

                // Write a track for each photo (pair of waypoints)
                for (int i = 0; i < waypoints.Count; i += 2)
                {
                    await writer.WriteStartElementAsync(null, "trk", null);

                    // Track name: use photo name
                    await writer.WriteStartElementAsync(null, "name", null);
                    await writer.WriteStringAsync(waypoints[i].Name.Replace(" (offset)", ""));
                    await writer.WriteEndElementAsync(); // name

                    await writer.WriteStartElementAsync(null, "trkseg", null);

                    // Write the original and offset waypoints as track points
                    await writer.WriteStartElementAsync(null, "trkpt", null);
                    await writer.WriteAttributeStringAsync(null, "lat", null, waypoints[i].Latitude.ToString("F6"));
                    await writer.WriteAttributeStringAsync(null, "lon", null, waypoints[i].Longitude.ToString("F6"));
                    await writer.WriteEndElementAsync(); // trkpt

                    await writer.WriteStartElementAsync(null, "trkpt", null);
                    await writer.WriteAttributeStringAsync(null, "lat", null, waypoints[i + 1].Latitude.ToString("F6"));
                    await writer.WriteAttributeStringAsync(null, "lon", null, waypoints[i + 1].Longitude.ToString("F6"));
                    await writer.WriteEndElementAsync(); // trkpt

                    await writer.WriteEndElementAsync(); // trkseg
                    await writer.WriteEndElementAsync(); // trk
                }

                // Write GPX footer
                await writer.WriteEndElementAsync(); // gpx
                await writer.WriteEndDocumentAsync();

                writer.Flush();
            }
            catch (Exception ex)
            {
                File.AppendAllText("gpx_write_errors.log", $"[{DateTime.Now}] Error writing GPX: {ex.Message}{Environment.NewLine}");
                throw;
            }
        }

        private void CreateEmptyGpxFile(string filePath)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                Encoding = System.Text.Encoding.UTF8
            };

            using var writer = XmlWriter.Create(filePath, settings);
            
            writer.WriteStartDocument();
            writer.WriteStartElement("gpx");
            writer.WriteAttributeString("version", "1.1");
            writer.WriteAttributeString("creator", "PhotoToGPX");
            writer.WriteAttributeString("xmlns", "http://www.topografix.com/GPX/1/1");
            writer.WriteEndElement(); // gpx
            writer.WriteEndDocument();
        }

        private void LogError(string logFilePath, string photoPath, string errorMessage)
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var logEntry = $"[{timestamp}] ERROR: {photoPath} - {errorMessage}";
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch
            {
                // If we can't log, just continue - don't fail the entire process
            }
        }
    }

    public class GpsWaypoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
