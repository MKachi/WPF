using Microsoft.Win32;
using MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DICOM.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        private string OpenFile(string title, string filter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = title;
            fileDialog.Filter = filter;

            bool? result = fileDialog.ShowDialog();
            if (ReferenceEquals(result, null) || !result.Value)
            {
                return string.Empty;
            }
            return fileDialog.FileName;
        }
        private string SaveFile(string title, string filter)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = title;
            fileDialog.Filter = filter;

            bool? result = fileDialog.ShowDialog();
            if (ReferenceEquals(result, null) || !result.Value)
            {
                return string.Empty;
            }
            return fileDialog.FileName;
        }

        private void LoadJpg()
        {
            string filePath = OpenFile("Select jpg file", "JPG File (*.jpg)|*.jpg*|All Files|*.*");
            if (filePath.Equals(string.Empty))
            {
                return;
            }

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnDemand;
            image.CreateOptions = BitmapCreateOptions.DelayCreation;
            image.DecodePixelWidth = 300;
            image.UriSource = new Uri(filePath);
            image.EndInit();
            DisplayImage = image;
        }
        private void LoadDCM()
        {
            string filePath = OpenFile("Select DCM file", "DICOM File (*.dcm)|*.dcm*|All Files|*.*");
            if (filePath.Equals(string.Empty))
            {
                return;
            }

            gdcm.ImageReader reader = new gdcm.ImageReader();
            reader.SetFileName(filePath);
            reader.Read();

            gdcm.Image image = reader.GetImage();
            PixelFormat format = PixelFormats.Gray8;
            int width = (int)image.GetDimension(0);
            int height = (int)image.GetDimension(1);
            int rawStride = (width * format.BitsPerPixel + 7) / 8;

            ushort[] pixel16 = new ushort[width * height];
            image.GetArray(pixel16);
            byte[] pixel8 = Array.ConvertAll(pixel16, p => (byte)(255 - p / 16));
            DisplayImage = BitmapSource.Create(width, height, 96, 96, format, null, pixel8, rawStride) as BitmapImage;
        }

        private void SaveJpg()
        {
            string filePath = SaveFile("Save jpg", "JPG File (*.jpg)|*.jpg*");
            if (filePath.Equals(string.Empty))
            {
                return;
            }

            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(DisplayImage));

            using (FileStream stream = new FileStream(filePath + ".jpg", FileMode.Create))
            {
                encoder.Save(stream);
                stream.Close();
            }
            MessageBox.Show("저장 완료");
        }
        private void SaveDCM()
        {
            string jpgPath = OpenFile("Select jpg file", "JPG File (*.jpg)|*.jpg*|All Files|*.*");
            string filePath = SaveFile("Save DCM", "DICOM File (*.dcm)|*.dcm*");
            if (filePath.Equals(string.Empty))
            {
                return;
            }

            //byte[] pixels;
            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(DisplayImage));
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    encoder.Save(stream);
            //    pixels = stream.ToArray();
            //    stream.Close();
            //}
            FileStream jstream = new FileStream(jpgPath, FileMode.Open);
            uint fileSize = gdcm.PosixEmulation.FileSize(jpgPath);

            byte[] pixels = new byte[fileSize];
            jstream.Read(pixels, 0, pixels.Length);

            gdcm.ImageWriter writer = new gdcm.ImageWriter();
            gdcm.Image image = new gdcm.Image();
            gdcm.DataElement pixelData = new gdcm.DataElement(new gdcm.Tag(0x7fe0, 0x0010));
            gdcm.SmartPtrFrag ptrFrag = gdcm.SequenceOfFragments.New();
            gdcm.Fragment frag = new gdcm.Fragment();

            frag.SetByteValue(pixels, new gdcm.VL((uint)pixels.Length));
            ptrFrag.AddFragment(frag);
            pixelData.SetValue(ptrFrag.__ref__());
            image.SetDataElement(pixelData);

            var pi = new gdcm.PhotometricInterpretation(gdcm.PhotometricInterpretation.PIType.YBR_FULL);
            image.SetPhotometricInterpretation(pi);

            gdcm.PixelFormat format = new gdcm.PixelFormat(3, 8, 8, 7);
            image.SetPixelFormat(format);

            image.SetTransferSyntax(new gdcm.TransferSyntax(gdcm.TransferSyntax.TSType.JPEGLosslessProcess14_1));
            image.SetDimension(0, (uint)DisplayImage.PixelWidth);
            image.SetDimension(1, (uint)DisplayImage.PixelHeight);

            byte[] decompressedData = new byte[(int)image.GetBufferLength()];
            image.GetBuffer(decompressedData);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                BinaryWriter binaryWriter = new BinaryWriter(stream);
                binaryWriter.Write(decompressedData);
                binaryWriter.Close();
                stream.Close();
            }
            MessageBox.Show("저장 완료");
        }
    }
}
