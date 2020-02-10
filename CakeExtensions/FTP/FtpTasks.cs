using Cake.Core;
using Cake.Core.Annotations;
using FTPCakeExtension;
using System;
using System.IO;
using System.Linq;
using System.Net;

public static class FTPTasks
{
    public static NetworkCredential FtpCredentials { get; set; }

    //public static void FtpUploadDirectoryRecursively(string directory, string hostRoot)
    //{
    //    var files = PathConstruction.GlobFiles(directory, "**/*").ToList();
    //    for (var index = 0; index < files.Count; index++)
    //    {
    //        var file = files[index];
    //        var relativePath = PathConstruction.GetRelativePath(directory, file);
    //        var hostPath = $"{hostRoot}/{relativePath}";

    //        FtpUploadFileInternal(file, hostPath, $"[{index + 1}/{files.Count}] ");
    //    }
    //}    

    public static void FtpUploadFile(string file, string hostDestination, NetworkCredential ftpCredentials)
    {
        if (FtpCredentials == null)
        {
            FtpCredentials = ftpCredentials;
        }

        FtpUploadFileInternal(file, hostDestination);
    }

    public static void FtpUploadFileInternal(string file, string hostDestination, string prefix = null)
    {
        ControlFlow.ExecuteWithRetry(() =>
        {
            FtpMakeDirectory(GetParentPath(hostDestination));

            var request = WebRequest.Create(hostDestination);
            request.Credentials = FtpCredentials;
            request.Method = WebRequestMethods.Ftp.UploadFile;

            var content = File.ReadAllBytes(file);
            request.ContentLength = content.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(content, offset: 0, count: content.Length);
            requestStream.Close();
        });
    }

    public static void FtpMakeDirectory(string path)
    {
        var parentPath = GetParentPath(path);
        if (parentPath != path)
            FtpMakeDirectory(parentPath);

        var request = WebRequest.Create(path);
        request.Method = WebRequestMethods.Ftp.MakeDirectory;
        request.Credentials = FtpCredentials;
        try
        {
            request.GetResponse().Dispose();
        }
        catch
        {
            // ignored
        }
    }

    private static string GetParentPath(string path)
    {
        var uri = new Uri(path);
        return uri.AbsoluteUri.Remove(uri.AbsoluteUri.Length - uri.Segments.Last().Length);
    }
}