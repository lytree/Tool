using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Entity
{
	public class Vib
	{

		public int id { get; set; }//取值：0—15，主键
		public Date saveTime { get; set; }//保存时间
		public long saveTime_Com { get; set; }//毫秒数
		public int dataId { get; set; }//数据类型0~15位；月时间，周时间......
		public int jc { get; set; }//报警状态；
		public float speed { get; set; } = 0f;//转速
		public float vib_rms { get; set; } = 0f;//振动有效值
		public float vib_p { get; set; } = 0f;//振动单峰值
		public float vib_pp { get; set; } = 0f;//振动峰峰值
		public float vib_vsx1_scale { get; set; } = 0f;//VIB_Vsx1与全频谱幅值平方和开方的比值
		public float vib_vsx2_scale { get; set; } = 0f;//VIB_Vsx2与全频谱幅值平方和开方的比值
		public float vib_vsx3_scale { get; set; } = 0f;
		public float vib_vsx4_scale { get; set; } = 0f;
		public float vib_vsx5_scale { get; set; } = 0f;
		public float vib_vsx6_scale { get; set; } = 0f;
		public float vib_vsx7_scale { get; set; } = 0f;
		public float vib_vsx8_scale { get; set; } = 0f;
		public float temperature { get; set; } = 0f; //温度
		public float temperature_rise { get; set; } = 0f; //温升

		public int vib_wave_len = 0;//压缩后的振动波形数据的字节长度
		public byte[] vib_wave { get; set; }

		public float tempValue;
	}
}
