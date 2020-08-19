using AppConfigurationManager;
using AppConfigurationManager.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Logger = EventLogManager.EventLog;
namespace BackupArchivizer
{
    class Program
    {
        static void Main()
        {
            var logger = new Logger();
            try
            {
                var configurationReader = new AppConfigurationReader();
                var configuration = configurationReader.ReadConfiguration();

                var fileToArchive = SelectFilesToArchive(configuration);
                logger.LogInfo("{0} files have been selected for packing", fileToArchive.Count);

                foreach (var fileInfo in fileToArchive)
                {
                    ArchiveFile(configuration, fileInfo);
                    DeleteSourceFile(configuration, fileInfo);
                }
                logger.LogInfo("Archiving complete");


                var archiveToDelete = SelectArchiveToDelete(configuration);
                logger.LogInfo($"Removing {archiveToDelete.Count} obsolete archives");
                foreach (var archiveInfo in archiveToDelete)
                {                    
                    File.Delete(archiveInfo.FullName);
                    logger.LogInfo($"File {archiveInfo.FullName} has deleted.");
                }
            }
            catch (Exception e)
            {
                logger.LogFatal(e, "");
            }
        }

        private static void DeleteSourceFile(ArchivizerConfiguration configuration, FileInfo fileInfo)
        {
            if (configuration.DeleteSourceFileAfterArchiving)
            {
                File.Delete(fileInfo.FullName);
            }
        }

        private static void ArchiveFile(ArchivizerConfiguration configuration, FileInfo fileInfo)
        {
            var configurationForDirectory = configuration.ArchivizerConfigurationsForDirectory[fileInfo.DirectoryName];
            var targetName = Path.Combine(fileInfo.DirectoryName, Path.GetFileNameWithoutExtension(fileInfo.FullName) + $".{configurationForDirectory.FormatArchiwum}");

            var p = new ProcessStartInfo() { FileName = configuration.Achivizer7zFullName, WindowStyle = ProcessWindowStyle.Hidden };

            p.Arguments = $"a -t{configurationForDirectory.FormatArchiwum} \"" + targetName + "\" \"" + fileInfo.FullName + $"\" -mx={(int)configurationForDirectory.CompressionLevel}";

            Process x = Process.Start(p);
            x.WaitForExit();
        }

        private static ReadOnlyCollection<FileInfo> SelectFilesToArchive(ArchivizerConfiguration configuration)
        {
            var result = new List<FileInfo>();

            foreach (var configurationForDirectory in configuration.ArchivizerConfigurationsForDirectory.Values)
            {
                var files = Directory.GetFiles(configurationForDirectory.DirectoryFullName);

                var archiveFiles = new HashSet<FileInfo>();
                var sourceFiles = new HashSet<FileInfo>();

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Extension == $".{configurationForDirectory.FormatArchiwum}")
                    {
                        archiveFiles.Add(fileInfo);
                    }
                    else if (fileInfo.Extension == $".{configurationForDirectory.FileExtensionToCompression}")
                    {
                        sourceFiles.Add(fileInfo);
                    }
                }

                var fileToArchive = sourceFiles.Except(archiveFiles, new FileInfoComparer()).ToList();
                result.AddRange(fileToArchive);
            }
            return new ReadOnlyCollection<FileInfo>(result);
        }

        private static ReadOnlyCollection<FileInfo> SelectArchiveToDelete(ArchivizerConfiguration configuration)
        {
            var archiveToDelete = new List<FileInfo>();
            if (!configuration.MaxNumberOfLatestArchiveFilesInKept.HasValue)
            {
                return new ReadOnlyCollection<FileInfo>(archiveToDelete);
            }            
            
            foreach (var configurationForDirectory in configuration.ArchivizerConfigurationsForDirectory.Values)
            {
                var subResult = new SortedDictionary<DateTime, List<FileInfo>>();
                var files = Directory.GetFiles(configurationForDirectory.DirectoryFullName);

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Extension == $".{configurationForDirectory.FormatArchiwum}")
                    {
                        var match = Regex.Match(fileInfo.Name, @"\d{4}_\d{2}_\d{2}");
                        if (match.Success)
                        {
                            var archiveCreateDate = DateTime.ParseExact(match.Value, @"yyyy_MM_dd",
                                new System.Globalization.CultureInfo("pl-PL"));

                            if (subResult.ContainsKey(archiveCreateDate))
                            {
                                subResult[archiveCreateDate].Add(fileInfo);
                            }
                            else
                            {
                                subResult.Add(archiveCreateDate, new List<FileInfo> { fileInfo });
                            }
                        }
                    }
                }

                var i = 1;
                foreach (var item in subResult.Reverse())
                {
                    if (i > configuration.MaxNumberOfLatestArchiveFilesInKept.Value)
                    {
                        archiveToDelete.AddRange(item.Value);
                    }
                    i++;
                }
            }
            return new ReadOnlyCollection<FileInfo>(archiveToDelete);
        }
    }
}