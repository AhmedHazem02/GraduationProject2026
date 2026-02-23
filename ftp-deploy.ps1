$ftpHost   = "ftp://site50674.siteasp.net"
$ftpUser   = "site50674"
$ftpPass   = "9Ti+e3@BZ!d5"
$localPath = "C:\publish\mentorly"
$remotePath = "/site/wwwroot"

$credential = New-Object System.Net.NetworkCredential($ftpUser, $ftpPass)

function Create-FtpDirectory($remoteDir) {
    try {
        $req = [System.Net.FtpWebRequest]::Create("$ftpHost$remoteDir")
        $req.Credentials  = $credential
        $req.Method       = [System.Net.WebRequestMethods+Ftp]::MakeDirectory
        $req.UsePassive   = $true
        $req.UseBinary    = $true
        $req.KeepAlive    = $false
        $res = $req.GetResponse()
        $res.Close()
    } catch { <# directory already exists - ignore #> }
}

function Upload-File($localFile, $remoteFile) {
    $req = [System.Net.FtpWebRequest]::Create("$ftpHost$remoteFile")
    $req.Credentials  = $credential
    $req.Method       = [System.Net.WebRequestMethods+Ftp]::UploadFile
    $req.UsePassive   = $true
    $req.UseBinary    = $true
    $req.KeepAlive    = $false

    $bytes = [System.IO.File]::ReadAllBytes($localFile)
    $req.ContentLength = $bytes.Length
    $stream = $req.GetRequestStream()
    $stream.Write($bytes, 0, $bytes.Length)
    $stream.Close()

    $res = $req.GetResponse()
    $res.Close()
}

# Create root directory just in case
Create-FtpDirectory $remotePath

$files = Get-ChildItem -Path $localPath -Recurse -File
$total = $files.Count
$i = 0

foreach ($file in $files) {
    $i++
    $relative   = $file.FullName.Substring($localPath.Length).Replace("\", "/")
    $remoteFile = "$remotePath$relative"
    $remoteDir  = $remoteFile.Substring(0, $remoteFile.LastIndexOf("/"))

    Create-FtpDirectory $remoteDir
    Write-Host "[$i/$total] Uploading: $relative"
    Upload-File $file.FullName $remoteFile
}

Write-Host ""
Write-Host "✅ Upload Complete! Visit: https://mentorly.runasp.net"
