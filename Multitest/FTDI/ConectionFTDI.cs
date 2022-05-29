using System;
using System.Text;
using System.Windows.Forms;
using FTD2XX_NET;
 

namespace Multitest.PanelesPrincipales
{
    public class ConectionFTDI
    {
        private bool port = false;

        private FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        private FTDI myFtdiDevice;
        private int conection = 0;

        public ConectionFTDI()
        {
            myFtdiDevice = new FTDI();
          //  openPort();

        }


        public bool openPort()
        {
            if (!myFtdiDevice.IsOpen)
            {


                UInt32 ftdiDeviceCount = 0;

                ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

                ///lista de dispositivos ftdi
                FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

                //asigna la lista de dispositivos
                ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);

                if (ftdiDeviceList.Length != 0)
                {

                    ftStatus = myFtdiDevice.OpenBySerialNumber(ftdiDeviceList[0].SerialNumber);


                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        ftStatus = myFtdiDevice.SetBaudRate(9600);
                        ftStatus = myFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8,
                            FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_NONE);
                        port = true;
                    }
                    else
                        conection = 2;
                   // MessageBox.Show("Ocurrio un error al abrir el puerto de comunicacion.Reinicie la aplicacion");
                }
                else
                   // MessageBox.Show("Verifique si el dispositivo esta conectado");
                conection = 1;
            }

            if (myFtdiDevice.IsOpen)
            {
                //int y = 0;
            }

            return port;

        }


        public bool Conectado()
        {
            bool conectado = false;
            UInt32 ftdiDeviceCount = 0;

            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

            ///lista de dispositivos ftdi
            FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

            //asigna la lista de dispositivos
            ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);
            if (ftdiDeviceList.Length != 0)
            {
                conectado = true;
            }

            return conectado;

        }


        public int sendCommandTRS(  )
        {

            bool send = false;
            int result = 0;
            if (myFtdiDevice.IsOpen)
            {
               // byte[] r = Encoding.ASCII.GetBytes("e");

                byte[] r = new byte[1];
               r[0] = 2;
          
                UInt32 numBytesWritten = 2;
                ftStatus = myFtdiDevice.Write(r, r.Length, ref numBytesWritten);


                byte[] rw = Encoding.ASCII.GetBytes("e");

                UInt32 numBytesWrittenw = 2;
                ftStatus = myFtdiDevice.Write(rw, rw.Length, ref numBytesWrittenw);

                send = true;
            }

            if (send == true)
            {
                UInt32 numBytesAvailable = 0;
                UInt32 numBytesExpected = 4;
                myFtdiDevice.Purge(0);
                while (numBytesAvailable < numBytesExpected)
                {

                    ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);

                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                        throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);


                }


                if (numBytesAvailable != numBytesExpected)
                    throw new Exception("Error: Invalid data in buffer. (1350)");


                UInt32 numBytesRead = 0;
                byte[] rawData = new byte[numBytesExpected];
                ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);


                if (BitConverter.IsLittleEndian)
                    Array.Reverse(rawData);


                result = BitConverter.ToInt32(rawData, 0);


                if (result % 2 != 0)
                {
                    result = result - (result * 2);
                }

            }

            return result;
        }

        public int sendCommandTRN()
        {

            bool send = false;
            int result = 0;
            if (myFtdiDevice.IsOpen)
            {
                // byte[] r = Encoding.ASCII.GetBytes("e");

                byte[] r = new byte[1];
                r[0] = 1;

                UInt32 numBytesWritten = 1;
                ftStatus = myFtdiDevice.Write(r, r.Length, ref numBytesWritten);
                 
                send = true;
            }

            if (send == true)
            {
                UInt32 numBytesAvailable = 0;
                UInt32 numBytesExpected = 2;
                myFtdiDevice.Purge(0);
                while (numBytesAvailable < numBytesExpected)
                {

                    ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);

                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                        throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);


                }





                if (numBytesAvailable != numBytesExpected)
                    throw new Exception("Error: Invalid data in buffer. (1350)");


                UInt32 numBytesRead = 0;
                byte[] rawData = new byte[numBytesExpected];
                ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);


                if (BitConverter.IsLittleEndian)
                    Array.Reverse(rawData);


                result = BitConverter.ToInt32(rawData, 0);


                if (result % 2 != 0)
                {
                    result = result - (result * 2);
                }

            }

            return result;
        }


        public int leerPuerto()
        {
            int valor = 0;
            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = 2;
            myFtdiDevice.Purge(0);
            while (numBytesAvailable < numBytesExpected)
            {

                ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);

                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                    throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);


            }





            if (numBytesAvailable != numBytesExpected)
                throw new Exception("Error: Invalid data in buffer. (1350)");


            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);


            if (BitConverter.IsLittleEndian)
                Array.Reverse(rawData);

            return valor;

        }

        public void closePort()
        {
            myFtdiDevice.Close();
        }

        public int GetCount()
        {
            return conection;
        }
    }
}