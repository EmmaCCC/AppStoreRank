using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace YQH.Tourism.Common.SMS
{
    public sealed class CertificateManager
    {
        private const string ROOT_CERT_BASE64 = "MIIF7DCCBNSgAwIBAgIQbsx6pacDIAm4zrz06VLUkTANBgkqhkiG9w0BAQUFADCByjELMAkGA1UEBhMCVVMxFzAVBgNVBAoTDlZlcmlTaWduLCBJbmMuMR8wHQYDVQQLExZWZXJpU2lnbiBUcnVzdCBOZXR3b3JrMTowOAYDVQQLEzEoYykgMjAwNiBWZXJpU2lnbiwgSW5jLiAtIEZvciBhdXRob3JpemVkIHVzZSBvbmx5MUUwQwYDVQQDEzxWZXJpU2lnbiBDbGFzcyAzIFB1YmxpYyBQcmltYXJ5IENlcnRpZmljYXRpb24gQXV0aG9yaXR5IC0gRzUwHhcNMTAwMjA4MDAwMDAwWhcNMjAwMjA3MjM1OTU5WjCBtTELMAkGA1UEBhMCVVMxFzAVBgNVBAoTDlZlcmlTaWduLCBJbmMuMR8wHQYDVQQLExZWZXJpU2lnbiBUcnVzdCBOZXR3b3JrMTswOQYDVQQLEzJUZXJtcyBvZiB1c2UgYXQgaHR0cHM6Ly93d3cudmVyaXNpZ24uY29tL3JwYSAoYykxMDEvMC0GA1UEAxMmVmVyaVNpZ24gQ2xhc3MgMyBTZWN1cmUgU2VydmVyIENBIC0gRzMwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCxh4QfwgxF9byrJZenraI+nLr2wTm4i8rCrFbG5btljkRPTc5v7QlK1K9OEJxoiy6Ve4mbE8riNDTB81vzSXtig0iBdNGIeGwCU/m8f0MmV1gzgzszChew0E6RJK2GfWQS3HRKNKEdCuqWHQsV/KNLO85jiND4LQyUhhDKtpo9yus3nABINYYpUHjoRWPNGUFP9ZXse5jUxHGzUL4os4+guVOc9cosI6n9FAboGLSa6Dxugf3kzTU2s1HTaewSulZub5tXxYsU5w7HnO1KVGrJTcW/EbGuHGeBy0RVM5l/JJs/U0V/hhrzPPptf4H1uErT9YU3HLWm0AnkGHs4TvoPAgMBAAGjggHfMIIB2zA0BggrBgEFBQcBAQQoMCYwJAYIKwYBBQUHMAGGGGh0dHA6Ly9vY3NwLnZlcmlzaWduLmNvbTASBgNVHRMBAf8ECDAGAQH/AgEAMHAGA1UdIARpMGcwZQYLYIZIAYb4RQEHFwMwVjAoBggrBgEFBQcCARYcaHR0cHM6Ly93d3cudmVyaXNpZ24uY29tL2NwczAqBggrBgEFBQcCAjAeGhxodHRwczovL3d3dy52ZXJpc2lnbi5jb20vcnBhMDQGA1UdHwQtMCswKaAnoCWGI2h0dHA6Ly9jcmwudmVyaXNpZ24uY29tL3BjYTMtZzUuY3JsMA4GA1UdDwEB/wQEAwIBBjBtBggrBgEFBQcBDARhMF+hXaBbMFkwVzBVFglpbWFnZS9naWYwITAfMAcGBSsOAwIaBBSP5dMahqyNjmvDz4Bq1EgYLHsZLjAlFiNodHRwOi8vbG9nby52ZXJpc2lnbi5jb20vdnNsb2dvLmdpZjAoBgNVHREEITAfpB0wGzEZMBcGA1UEAxMQVmVyaVNpZ25NUEtJLTItNjAdBgNVHQ4EFgQUDURcFlNEwYJ+HSCrJfQBY9i+eaUwHwYDVR0jBBgwFoAUf9Nlp8Ld7LvwMAnzQzn6Aq8zMTMwDQYJKoZIhvcNAQEFBQADggEBAAyDJO/dwwzZWJz+NrbrioBL0aP3nfPMU++CnqOh5pfBWJ11bOAdG0z60cEtBcDqbrIicFXZIDNAMwfCZYP6j0M3m+oOmmxw7vacgDvZN/R6bezQGH1JSsqZxxkoor7YdyT3hSaGbYcFQEFn0Sc67dxIHSLNCwuLvPSxe/20majpdirhGi2HbnTTiN0eIsbfFrYrghQKlFzyUOyvzv9iNw2tZdMGQVPtAhTItVgooazgW+yzf5VK+wPIrSbb5mZ4EkrZn0L74ZjmQoObj49nJOhhGbXdzbULJgWOw27EyHW4Rs/iGAZeqa6ogZpHFt4MKGwlJ7net4RYxh84HqTEy2Y=";
        private static readonly IDictionary<string, bool> chuanglanDomains = new Dictionary<string, bool>();
        private static X509Certificate2 rootCert;

        static CertificateManager()
        {
            chuanglanDomains.Add("*.253.com", true);

            byte[] rootCertData = Convert.FromBase64String(ROOT_CERT_BASE64);
            rootCert = new X509Certificate2(rootCertData);
        }

        private static bool TrustAllValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; // 忽略SSL证书检查
        }

        private static bool ChuanglanCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            foreach (X509ChainElement element in chain.ChainElements)
            {
                if (!element.Certificate.Verify())
                {
                    return false;
                }
            }

            bool hasChuanglanDomain = chuanglanDomains.ContainsKey(GetCertificateCN(certificate));
            if (!hasChuanglanDomain)
            {
                throw new SmsException("Access to the non 253's HTTPS services are not allowed!");
            }

            X509Chain rootChain = new X509Chain();
            rootChain.ChainPolicy.ExtraStore.Add(rootCert);
            return rootChain.Build((X509Certificate2)certificate);
        }

        private static string GetCertificateCN(X509Certificate cert)
        {
            string subject = cert.Subject;
            string[] entries = subject.Split(',');
            foreach (string entry in entries)
            {
                string[] kv = entry.Trim().Split('=');
                if ("CN".Equals(kv[0]) && kv.Length > 1)
                {
                    return kv[1];
                }
            }
            return subject;
        }

        private static RemoteCertificateValidationCallback allCallback = new RemoteCertificateValidationCallback(TrustAllValidationCallback);
        private static RemoteCertificateValidationCallback chuanglanCallback = new RemoteCertificateValidationCallback(ChuanglanCallback);

        public static RemoteCertificateValidationCallback GetCallback(bool ignoreSSLCheck)
        {
            if (ignoreSSLCheck)
            {
                return allCallback;
            }
            else
            {
                return chuanglanCallback;
            }
        }
    }
}
