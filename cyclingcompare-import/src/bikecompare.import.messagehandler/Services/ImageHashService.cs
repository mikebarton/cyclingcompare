using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace bikecompare.import.messagehandler.Services
{
	public class ImageHashService
	{
		private readonly HttpClient _httpClient;

        public ImageHashService(HttpClient httpClient)
        {
			_httpClient = new HttpClient();
        }

		public async Task<string> GetHash(string url)
		{
			try
			{
				using (var response = await _httpClient.GetAsync(url))
				{
					response.EnsureSuccessStatusCode();
					using (var imageStream = await response.Content.ReadAsStreamAsync())
					{
						using (var image = (Bitmap)Image.FromStream(imageStream))
							return await GetHash(image);
					}
				}
			}catch(Exception e)
            {
				Console.WriteLine($"error downloading: {e.Message}");
				//throw;
				return null;
            }
		}

		public async Task<string> GetHash(Bitmap bitmap)
		{
			var formatter = new BinaryFormatter();

			using (var memoryStream = new MemoryStream())
			{
				//var metafields = GetMetaFields(bitmap).ToArray();

				//if (metafields.Any())
				//	formatter.Serialize(memoryStream, metafields);

				var pixelBytes = GetPixelBytes(bitmap);
				memoryStream.Write(pixelBytes, 0, pixelBytes.Length);

				using (var hashAlgorithm = GetHashAlgorithm())
				{
					memoryStream.Seek(0, SeekOrigin.Begin);
					var hash = await hashAlgorithm.ComputeHashAsync(memoryStream);
					return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
				}
			}
		}

		private static HashAlgorithm GetHashAlgorithm() => MD5.Create();

		private static byte[] GetPixelBytes(Bitmap bitmap, PixelFormat pixelFormat = PixelFormat.Format32bppRgb)
		{
			var lockedBits = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, pixelFormat);

			var bufferSize = lockedBits.Height * lockedBits.Stride;
			var buffer = new byte[bufferSize];
			Marshal.Copy(lockedBits.Scan0, buffer, 0, bufferSize);

			bitmap.UnlockBits(lockedBits);

			return buffer;
		}

		//private static IEnumerable<KeyValuePair<string, string>> GetMetaFields(Image image)
		//{
		//	string manufacturer = System.Text.Encoding.ASCII.GetString(image.PropertyItems[1].Value);

		//	yield return new KeyValuePair<string, string>("manufacturer", manufacturer);

		//	// return any other fields you may be interested in

		//}
	}
}
