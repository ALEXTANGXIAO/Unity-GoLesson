using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Message
{
    private const int bufferHead = 4;
    private static  byte[] buffer = new byte[1024];
    private int startindex;

    public byte[] Buffer
    {
        get { return buffer; }
    }

    public int StartIndex
    {
        get { return startindex; }
    }

    public int Remsize
    {
        get { return buffer.Length - startindex; }
    }

    public void ReadBuffer(int length, Action callbackAction = null)
    {
        startindex += length;

        if (startindex <= bufferHead)
        {
            return;
        }

        int count = BitConverter.ToInt32(buffer, 0);

        int buffTotalCount = count + bufferHead;

        while (true)
        {
            if (startindex >= buffTotalCount)
            {
                //todo 解包 拿到咱们的proto
                //callbackAction?.Invoke();

                Array.Copy(buffer,buffTotalCount,buffer,0,startindex - buffTotalCount);

                startindex -= buffTotalCount;
            }
            else
            {
                break;
            }
        }
    }
}