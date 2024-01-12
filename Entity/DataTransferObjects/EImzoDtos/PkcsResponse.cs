using System.Text.Json.Serialization;
using EimzoApi.Models;

namespace EimzoApi.Models
{
    public partial class PkcsResponse
    {
        [JsonPropertyName("pkcs7Info")]
        public Pkcs7InfoModel Pkcs7Info { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }

    public partial class Pkcs7InfoModel
    {
        [JsonPropertyName("signers")]
        public List<SignerModel> Signers { get; set; }

        [JsonPropertyName("documentBase64")]
        public string DocumentBase64 { get; set; }
    }

    public class SignerModel
    {
       
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        [JsonPropertyName("signingTime")]
        public DateTime SigningTime { get; set; }

        [JsonPropertyName("timeStampInfo")]
        public TimeStampInfoModel TimeStampInfo { get; set; }

        [JsonPropertyName("certificate")]
        public List<CertificateModel> Certificate { get; set; }

        [JsonPropertyName("verified")]
        public bool Verified { get; set; }

        [JsonPropertyName("certificateVerified")]
        public bool CertificateVerified { get; set; }

        [JsonPropertyName("policyIdentifiers")]
        public List<string> PolicyIdentifiers { get; set; }
    }

    public partial class CertificateModel
    {
        [JsonPropertyName("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonPropertyName("subjectName")]
        public string SubjectName { get; set; }

        [JsonPropertyName("validFrom")]
        public DateTime ValidFrom { get; set; }

        [JsonPropertyName("validTo")]
        public DateTime ValidTo { get; set; }

        [JsonPropertyName("issuerName")]
        public string IssuerName { get; set; }

        [JsonPropertyName("publicKey")]
        public PublicKeyModel PublicKey { get; set; }

        [JsonPropertyName("signature")]
        public SignatureModel Signature { get; set; }
    }

    public partial class PublicKeyModel
    {
        [JsonPropertyName("keyAlgName")]
        public string KeyAlgName { get; set; }

        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }
    }

    public partial class SignatureModel
    {
        [JsonPropertyName("signAlgName")]
        public string SignAlgName { get; set; }

        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }

    public partial class TimeStampInfoModel
    {
        [JsonPropertyName("certificate")]
        public List<CertificateModel> Certificate { get; set; }
        [JsonPropertyName("digestVerified")]
        public bool DigestVerified { get; set; }

    }
}
