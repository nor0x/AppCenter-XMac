#addin "nuget:?package=System.Xml.ReaderWriter"

using System.Xml;
using System.Collections.Generic;

// This class contains the assembly folder paths and other platform dependent paths involved in preparing assemblies for VSTS and Azure storage.
// When a new platform is supported, an AssemblyGroup must be created and added to the proper {OS}UploadAssemblyGroups array. Also, its
// AssemblyFolder must be added to the correct platform's "DownloadAssemblyFolders" array.
public class PlatformPaths
{
    public PlatformPaths(string uploadAssembliesZip, string downloadAssembliesZip, string downloadUrl, string configFilePath)
    {
        UploadAssembliesZip = uploadAssembliesZip;
        DownloadAssembliesZip = downloadAssembliesZip;
        DownloadUrl = downloadUrl;
        UploadAssemblyGroups = new List<AssemblyGroup>();
        DownloadAssemblyGroups = new List<AssemblyGroup>();
        var groups = AssemblyGroup.ReadAssemblyGroups(configFilePath);
        AddGroups(groups);
    }

    private void AddGroups(IList<AssemblyGroup> groups)
    {
        foreach (var group in groups)
        {
            AddGroups(group.Subgroups);
            if (group.AssemblyPaths.Count > 0)
            {
                if (group.Download)
                {
                    DownloadAssemblyGroups.Add(group);
                }
                else
                {
                    UploadAssemblyGroups.Add(group);
                }
            }
        }
    }

    // Folders for the assemblies that the current platform must create and upload
    public List<AssemblyGroup> UploadAssemblyGroups { get; set; }

    // The downloaded assembly groups
    public List<AssemblyGroup> DownloadAssemblyGroups { get; set; }

    // The name of the zip file to upload
    public string UploadAssembliesZip { get; set; }

    // The name of the zip file to download
    public string DownloadAssembliesZip { get; set; }

    // The URL to download files from
    public string DownloadUrl { get; set; }
}

// AppCenter module class definition.
class AppCenterModule {
    public string AndroidModule { get; set; }
    public string DotNetModule { get; set; }
    public string NuGetVersion { get; set; }
    public string PackageId { get; set; }
    public string MainNuspecFilename { get; set; }
    public string NuGetPackageName => $"{PackageId}.{NuGetVersion}.nupkg";
    public string MacNuspecFilename =>  $"Mac" + MainNuspecFilename;
    public string WindowsNuspecFilename => "Windows" + MainNuspecFilename;
    public AppCenterModule(string android, string dotnet, string mainNuspecFilename) {
        AndroidModule = android;
        DotNetModule = dotnet;
        MainNuspecFilename = mainNuspecFilename;
    }

    private AppCenterModule(XmlNode moduleNode)
    {
        
    }
}

public class AssemblyGroup
{
    public static IList<AssemblyGroup> ReadAssemblyGroups(string configFilePath)
    {
        XmlReader reader = XmlReader.Create(configFilePath);
        List<AssemblyGroup> groups = new List<AssemblyGroup>();
        while (reader.Read())
        {
            if (reader.Name == "group")
            {
                XmlDocument group = new XmlDocument();
                var node = group.ReadNode(reader);
                groups.Add(new AssemblyGroup(node));
            }
        }
        return groups;
    }

    public string NuspecKey { get; set; }
    public string Id { get; set; }
    public string Folder { get; set; }
    public IList<string> AssemblyPaths { get; set; }
    public IList<AssemblyGroup> Subgroups { get; set; }
    public bool Download { get; set; }
    private AssemblyGroup(XmlNode groupNode, AssemblyGroup parent = null)
    {
        string downloadString = Statics.Context.IsRunningOnUnix() ? "downloadMac" : "downloadWindows";
        AssemblyPaths = new List<string>();
        Subgroups = new List<AssemblyGroup>();
        Id = groupNode.Attributes.GetNamedItem("id").Value;
        NuspecKey = groupNode.Attributes.GetNamedItem("nuspecKey")?.Value;
        var downloadValue = groupNode.Attributes.GetNamedItem(downloadString)?.Value;
        if (downloadValue != null)
        {
            Download = (downloadValue == "true");
        }
        else if (parent != null)
        {
            Download = parent.Download;
        }
        string parentFolder = parent?.Folder ?? string.Empty;
        Folder = groupNode.Attributes.GetNamedItem("folder")?.Value ?? string.Empty;
        Folder = Statics.TemporaryPrefix + System.IO.Path.Combine(parentFolder, Folder);
        for (int i = 0; i < groupNode.ChildNodes.Count; ++i)
        {
            var childNode = groupNode.ChildNodes.Item(i);
            if (childNode.Name == "assembly")
            {
                AssemblyPaths.Add(childNode.Attributes.GetNamedItem("path").Value);
            }
            else if (childNode.Name == "group")
            {
                Subgroups.Add(new AssemblyGroup(childNode, this));
            }
        }
    }
}


