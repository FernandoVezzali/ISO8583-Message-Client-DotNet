using Microsoft.VisualStudio.TestTools.UnitTesting;
using imohsenb.iso8583.builders;
using imohsenb.iso8583.entities;
using imohsenb.iso8583.enums;
using imohsenb.iso8583.utils;

namespace Iso8583Test
{
	[TestClass]
	public class PackerTests
    {
		string message = "123456789001002020058000C00001930000123456123F123F123132333435363738313233343536373839303132333435AB6A53FC655F1487";

		[TestMethod]
		public void TestPackMessage()
		{
			ISOMessage iso = ISOMessageBuilder.Packer(VERSION.V1987)
				.Authorization()
				.MTI(MESSAGE_FUNCTION.Request, MESSAGE_ORIGIN.Acquirer)
				.ProcessCode("930000")
				.SetField(FIELDS.F11_STAN, "123456")
				.SetField(FIELDS.F22_EntryMode, "123F")
				.SetField(FIELDS.F24_NII_FunctionCode, "123F")
				.SetField(FIELDS.F25_POS_ConditionCode, "12")
				.SetField(FIELDS.F41_CA_TerminalID, "12345678")
				.SetField(FIELDS.F42_CA_ID, "123456789012345")
				.GenerateMac(data =>
				{
					return StringUtil.HexStringToByteArray("AB6A53FC655F1487");
				})
				.SetHeader("1234567890")
				.Build();

			Assert.AreEqual(message, iso.ToString());
		}

		[TestMethod]
		public void TestPackFields()
		{
			ISOMessage iso = ISOMessageBuilder.Packer(VERSION.V1987)
				.Authorization()
				.MTI(MESSAGE_FUNCTION.Request, MESSAGE_ORIGIN.Acquirer)
				.ProcessCode("930000")
				.SetField(FIELDS.F11_STAN, "123456")
				.SetField(FIELDS.F22_EntryMode, "123F")
				.SetField(FIELDS.F24_NII_FunctionCode, "123F")
				.SetField(FIELDS.F25_POS_ConditionCode, "12")
				.SetField(FIELDS.F41_CA_TerminalID, "12345678")
				.SetField(FIELDS.F42_CA_ID, "123456789012345")
				.GenerateMac(data =>
				{
					return StringUtil.HexStringToByteArray("AB6A53FC655F1487");
				})
				.SetHeader("1234567890")
				.Build();

			Assert.AreEqual("123456", iso.GetStringField(FIELDS.F11_STAN));
		}


		[TestMethod]
		public void TestUnpackMessage()
		{
			ISOMessage iso = ISOMessageBuilder
				.Unpacker()
				.SetMessage(message)
				.Build();

			Assert.AreEqual(message, iso.ToString());
		}
	}
}
