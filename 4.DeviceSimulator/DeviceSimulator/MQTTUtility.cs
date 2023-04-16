namespace Mock.MQTT
{
    static public class MQTTUtility
    {
        static short CRC16(byte[] data, int start, int end)
        {
            uint CRC = 0xFFFF;
            int i = 0, j = 0;
            for (i = start; i < end; i++)
            {
                CRC = CRC ^ data[i];
                for (j = 0; j < 8; j++)
                {
                    if ((CRC & 0x01) != 0)
                        CRC = ((CRC >> 1) ^ 0xA001);
                    else
                        CRC = CRC >> 1;
                }
            }
            return (short)(CRC);
        }
        static public byte[] MyEncode(byte[] data)
        {
            byte[] res = new byte[2 + data.Length];
            var crc = CRC16(data, 0, data.Length);
            res[0] = (byte)(crc & 0xFF);
            res[1] = (byte)((crc >> 8) & 0xFF);
            Array.Copy(data, 0, res, 2, data.Length);
            return res;
        }
        static public byte[] MyDecode(byte[] data)
        {
            if (data.Length < 2)
            {
                throw new Exception("内容长度异常");
            }
            byte[] res = new byte[data.Length - 2];
            var crc = CRC16(data, 2, data.Length);
            if ((crc & 0xFFFF) != (data[0] & 0xFF | (data[1] & 0xFF) << 8))
            {
                throw new Exception("数据无法通过校验");
            }
            Array.Copy(data, 2, res, 0, data.Length - 2);
            return res;
        }
    }
}