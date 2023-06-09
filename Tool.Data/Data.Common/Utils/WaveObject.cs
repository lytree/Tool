using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils;

public class WaveObject
{
	public int waveType { get; private set; }
	public int comtype;
	public int endianness;
	public float[] b = { 0, 1, 0 }; // 校正参数
	public float[] p = { 0, 1, 0 }; // 率定参数
	public float Speed { get; private set; }
	public float Freq { get; set; }
	public int Cycles { get; set; }
	public int Samples { get; set; }
	public int Valuetype { get; private set; } = 6;
	public int WaveLen { get; private set; }
	public float[] Wave { get; private set; }
	public byte[] WaveByte { get; private set; }

	public WaveObject Parse(byte[] vibwave)
	{
		IByteBuffer buffer = Unpooled.WrappedBuffer(vibwave);
		waveType = buffer.ReadByte();
		comtype = buffer.ReadByte();//是否压缩
		endianness = buffer.ReadByte();//字节序   3
		b[0] = buffer.ReadFloat();
		b[1] = buffer.ReadFloat();
		b[2] = buffer.ReadFloat();
		p[0] = buffer.ReadFloat();
		p[1] = buffer.ReadFloat();
		p[2] = buffer.ReadFloat();
		Speed = buffer.ReadFloat();//转速
		Freq = buffer.ReadFloat();//频率  32
		Cycles = buffer.ReadInt();
		Samples = buffer.ReadInt();
		Valuetype = buffer.ReadInt();
		WaveLen = buffer.ReadInt();// 16
		byte[] waveByte = new byte[WaveLen];

		//int Read = buffer.Read(waveByte);
		buffer.ReadBytes(waveByte);
		if (comtype == 1)
		{
			waveByte = ZLibUtils.Decompress(waveByte);
		}
		GetWave(waveByte);
		return this;
	}


	private static byte[] ToByte(float[] floats)
	{
		IByteBuffer bufferByte = Unpooled.Buffer(floats.Length * 4);
		foreach (float f in floats)
		{
			bufferByte.WriteFloatLE(f);
		}
		return bufferByte.Array;
	}
	public WaveObject? CreatWave(float[]? data, float freq, int samples, float speed = 0, int cycles = 0)
	{
		if (data == null)
		{
			return null;
		}
		byte comtype = 0; // 压缩方式：0：不压缩；1：zip
		byte endianness = 1; // 字节序：0：主机字节序；1：网络字节序

		IByteBuffer dos = Unpooled.WrappedBuffer(ToByte(data));
		dos.WriteByte(0);
		dos.WriteByte(comtype);
		dos.WriteByte(endianness);
		foreach (float bb in b)
		{
			dos.WriteFloat(bb);
		}
		foreach (float pp in p)
		{
			dos.WriteFloat(pp);
		}
		dos.WriteFloat(speed);
		dos.WriteFloat(freq);
		dos.WriteInt(cycles);
		dos.WriteInt(samples);
		dos.WriteInt(Valuetype);
		dos.WriteInt(samples * 4);
		foreach (float f in data)
		{
			dos.WriteFloat(f);
		}
		Cycles = cycles;
		Speed = speed;
		Freq = freq;
		Samples = samples;
		WaveByte = dos.Array;
		return this;
	}

	public (float[], float[], float[]) WaveFFT(float freq)
	{
		int spectral = Wave.Length;
		int mi = 0;
		for (; spectral > 1; spectral >>= 1)
		{
			mi++;
		}
		int nPointNumber = 1;
		int i;
		for (i = 0; i < mi; i++)
		{
			nPointNumber *= 2;
		}
		float[] temp = new float[nPointNumber];
		for (i = 0; i < temp.Length; i++)
		{
			temp[i] = Wave[i];
		}
		Wave = temp;

		int pBegin = 0;
		int nCount = Wave.Length;
		float fDF = freq / nPointNumber;

		// 抽取
		float[] x_arr = new float[nPointNumber + 1];
		float[] y_arr = new float[nPointNumber + 1];
		x_arr[0] = 0; // 插入空白点
		for (i = 0; i < (nPointNumber - 1); i++)
		{
			int pos = i;
			pos = (int)((long)pos * (nCount - 1) / nPointNumber);
			int ipos0 = pos;
			int ipos1 = ipos0 + 1;
			float v0 = Wave[pBegin + ipos0];
			float v1 = Wave[pBegin + ipos1];
			// 插值
			float t = pos - ipos0;
			float v = v0 + (v1 - v0) * t;
			x_arr[i + 1] = v;
		}
		x_arr[i + 1] = Wave[pBegin + nCount - 1]; // 插入最后一个点

		// 计算平均值average，并且令x[i] -= average以消除直流分量
		float sum = 0;
		for (i = 1; i < x_arr.Length; i++)
		{
			sum += x_arr[i];
		}
		float average = sum / nPointNumber;
		for (i = 1; i < x_arr.Length; i++)
		{
			x_arr[i] -= average;
		}

		O_fft(1, mi, x_arr, y_arr, nPointNumber);

		// 处理计算结果
		int number = (int)(nPointNumber / 2.56);
		float[] OutX = new float[number];
		float[] OutY = new float[number];
		float[] OutPhase = new float[number];
		for (i = 0; i < number; i++)
		{
			float xi = x_arr[i + 1];
			float yi = y_arr[i + 1];
			// 频率
			OutX[i] = i * fDF;
			// 幅值 mantis 695 *4 改为 *2
			OutY[i] = (float)(Math.Sqrt(xi * xi + yi * yi) * 2 / nPointNumber);
			// 相位 (弧度)
			OutPhase[i] = (float)Math.Atan2(yi, xi);
		}
		//Map map = new HashMap();
		//map.put("OutX", OutX);
		//map.put("OutY", OutY);
		//map.put("OutPhase", OutPhase);
		return (OutX, OutY, OutPhase);
	}

	private void O_fft(float dir, float i_dianshu_mi, float[] x, float[] y, int i_dianshu)
	{
		int i;
		int j;
		int k;
		int l;
		int m;
		int l1;
		float t1;
		float t2;
		float u1;
		float u2;
		float w1;
		float w2;
		float p2;
		float z;

		// Calculate the number of points
		if (i_dianshu == 0)
		{
			i_dianshu = 1;
			for (i = 0; i < i_dianshu_mi; i++)
			{
				i_dianshu *= 2;
			}
		}

		for (i = 0; i <= i_dianshu; i++)
		{
			y[i] = 0;
		}

		j = 1;

		for (l = 1; l <= (i_dianshu - 1); l++)
		{
			if (l < j)
			{
				t1 = x[j];
				t2 = y[j];
				x[j] = x[l];
				y[j] = y[l];
				x[l] = t1;
				y[l] = t2;
			}
			k = (i_dianshu) >> 1;
			while (k < j)
			{
				j -= k;
				k = k >> 1;
			}
			j = j + k;
		}
		m = 1;

		for (i = 1; i <= i_dianshu_mi; i++)
		{
			u1 = 1;
			u2 = 0;
			k = m;
			m = m << 1;
			p2 = 3.1415926f / k;
			w1 = (float)(Math.Cos(p2));
			w2 = (float)(-Math.Sin(p2));
			w2 = -w2;
			for (j = 1; j <= k; j++)
			{
				for (l = j; l <= i_dianshu; l += m)
				{
					l1 = l + k;
					t1 = x[l1] * u1 - y[l1] * u2;
					t2 = x[l1] * u2 + y[l1] * u1;
					x[l1] = x[l] - t1;
					y[l1] = y[l] - t2;
					x[l] += t1;
					y[l] += t2;
				}
				z = u1 * w1 - u2 * w2;
				u2 = u1 * w2 + u2 * w1;
				u1 = z;
			}
		}
	}

	//type为1，需要加汉宁窗
	public (float[], float[], float[]) WaveFFT(float freq, int type)
	{
		int nPointNumber = 1;
		int i;
		//data加了汉宁窗
		if (type == 1)
		{
			for (i = 0; i < Wave.Length; i++)
			{
				Wave[i] = (float)(Wave[i] * (0.5 - 0.5 * Math.Cos(2 * Math.PI * i / Wave.Length)));
			}
		}
		int spectral = Wave.Length;
		int mi = 0;
		for (; spectral > 1; spectral >>= 1)
		{
			mi++;
		}
		for (i = 0; i < mi; i++)
		{
			nPointNumber *= 2;
		}
		float[] temp = new float[nPointNumber];
		for (i = 0; i < temp.Length; i++)
		{
			temp[i] = Wave[i];
		}
		Wave = temp;

		int pBegin = 0;
		int nCount = Wave.Length;
		float fDF = freq / nPointNumber;

		// 抽取
		float[] x_arr = new float[nPointNumber + 1];
		float[] y_arr = new float[nPointNumber + 1];
		x_arr[0] = 0; // 插入空白点
		for (i = 0; i < (nPointNumber - 1); i++)
		{
			int pos = i;
			pos = (int)((long)pos * (nCount - 1) / nPointNumber);
			int ipos0 = pos;
			int ipos1 = ipos0 + 1;
			float v0 = Wave[pBegin + ipos0];
			float v1 = Wave[pBegin + ipos1];
			// 插值
			float t = pos - ipos0;
			float v = v0 + (v1 - v0) * t;
			x_arr[i + 1] = v;
		}
		x_arr[i + 1] = Wave[pBegin + nCount - 1]; // 插入最后一个点

		// 计算平均值average，并且令x[i] -= average以消除直流分量
		float sum = 0;
		for (i = 1; i < x_arr.Length; i++)
		{
			sum += x_arr[i];
		}
		float average = sum / nPointNumber;
		for (i = 1; i < x_arr.Length; i++)
		{
			x_arr[i] -= average;
		}

		O_fft(1, mi, x_arr, y_arr, nPointNumber);

		// 处理计算结果
		int number = (int)(nPointNumber / 2.56);
		float[] OutX = new float[number];
		float[] OutY = new float[number];
		float[] OutPhase = new float[number];
		for (i = 0; i < number; i++)
		{
			float xi = x_arr[i + 1];
			float yi = y_arr[i + 1];
			// 频率
			OutX[i] = i * fDF;
			// 幅值 mantis 695 *4 改为 *2
			OutY[i] = (float)(Math.Sqrt(xi * xi + yi * yi) * 2 / nPointNumber);
			// 相位 (弧度)
			OutPhase[i] = (float)Math.Atan2(yi, xi);
		}
		//Map map = new HashMap();
		//map.put("OutX", OutX);
		if (type == 1)
		{
			for (i = 0; i < OutY.Length; i++)
			{
				OutY[i] *= 2;
			}
		}
		//map.put("OutY", OutY);
		//map.put("OutPhase", OutPhase);
		return (OutX, OutY, OutPhase);
	}
	private void GetWave(byte[] waveByte)
	{
		IByteBuffer bufferByte = Unpooled.WrappedBuffer(waveByte);
		if (endianness == 1)
		{
			float a = b[1] * p[1];
			float c = p[1] * b[0] + p[0];
			switch (Valuetype)
			{
				case 0:
				case 3:
					Wave = new float[WaveLen];
					for (int i = 0; i < WaveLen; i++)
					{
						Wave[i] = bufferByte.ReadByte() * a + c;
					}
					break;
				case 1:
				case 4:
					Wave = new float[WaveLen / 2];
					for (int i = 0; i < WaveLen / 2; i++)
					{
						Wave[i] = bufferByte.ReadShort() * a + c;
					}
					break;
				case 2:
				case 5:
					Wave = new float[WaveLen / 4];
					for (int i = 0; i < WaveLen / 4; i++)
					{
						Wave[i] = bufferByte.ReadInt() * a + c;
					}
					break;
				case 6:
					Wave = new float[WaveLen / 4];
					for (int i = 0; i < WaveLen / 4; i++)
					{
						Wave[i] = bufferByte.ReadFloat() * a + c;
					}
					break;
				case 7:
					Wave = new float[WaveLen / 8];
					for (int i = 0; i < WaveLen / 8; i++)
					{
						Wave[i] = (float)bufferByte.ReadDouble() * a + c;
					}
					break;
			}
		}
		else
		{
			float a = b[1] * p[1];
			float c = p[1] * b[0] + p[0];
			switch (Valuetype)
			{
				case 0:
				case 3:
					Wave = new float[WaveLen];
					for (int i = 0; i < WaveLen; i++)
					{
						Wave[i] = bufferByte.ReadByte() * a + c;
					}
					break;
				case 1:
				case 4:
					Wave = new float[WaveLen / 2];
					for (int i = 0; i < WaveLen / 2; i++)
					{
						Wave[i] = bufferByte.ReadShortLE() * a + c;
					}
					break;
				case 2:
				case 5:
					Wave = new float[WaveLen / 4];
					for (int i = 0; i < WaveLen / 4; i++)
					{
						Wave[i] = bufferByte.ReadIntLE() * a + c;
					}
					break;
				case 6:
					Wave = new float[WaveLen / 4];
					for (int i = 0; i < WaveLen / 4; i++)
					{
						Wave[i] = bufferByte.ReadFloatLE() * a + c;
					}
					break;
				case 7:
					Wave = new float[WaveLen / 8];
					for (int i = 0; i < WaveLen / 8; i++)
					{
						Wave[i] = (float)bufferByte.ReadDoubleLE() * a + c;
					}
					break;
			}
		}
	}
}