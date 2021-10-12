if ($args.Count -ne 4)
{
  Write-Host "Usage: GenerateConnectionStrings.ps1 <output name> <connection #1 name> <connection #1 datasource> <connection #1 initialCatalog>"
  throw "Improper invocation: $args"
}

$filePath = $args[0]

$connectionName1 = $args[1]
$dataSource1 = $args[2]
$initialCatalog1 =$args[3]

$XmlWriter = New-Object System.XML.XmlTextWriter($filePath,$Null)
$XmlWriter.Formatting = "Indented"
$XmlWriter.Indentation = "4"

$XmlWriter.WriteStartElement("connectionStrings")

$XmlWriter.WriteStartElement("add")
$XmlWriter.WriteAttributeString("name","$connectionName1")
$XmlWriter.WriteAttributeString("connectionString","Data Source=$dataSource1;Initial Catalog=$initialCatalog1;Integrated Security=SSPI;")
$XmlWriter.WriteAttributeString("providerName","System.Data.SqlClient")
$XmlWriter.WriteEndElement();

$XmlWriter.WriteEndElement()

$XmlWriter.Flush();
$XmlWriter.Close();