using System;
using System.IO.Ports;
using Modbus.Device;

namespace ModbusTest
{
    public class Modbus
    {
        private byte slaveId;

        public Modbus(int slaveId)
        {
            this.slaveId =  (byte) slaveId;
        }
        
        public int GetInputRegisterUInt16(ushort startAddress)
        {
            using var serialPort = new SerialPort("COM3")
            {
                BaudRate = 9600, DataBits = 8, Parity = Parity.None, StopBits = StopBits.One
            };
            serialPort.Open();

            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

            ushort numRegisters = 1;
            
            var registers = master.ReadInputRegisters(slaveId, startAddress, numRegisters);

            return Convert.ToInt32(registers[0]);
        }
        
        public int GetInputRegisterUInt32(ushort startAddress)
        {
            using var serialPort = new SerialPort("COM3")
            {
                BaudRate = 9600, DataBits = 8, Parity = Parity.None, StopBits = StopBits.One
            };
            serialPort.Open();

            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

            ushort numRegisters = 2;
            
            var registers = master.ReadInputRegisters(slaveId, startAddress, numRegisters);

            return Convert.ToInt32(registers[0]) + (Convert.ToInt32(registers[1]) * 256);
        }

        public int GetHoldingRegisterUInt16(ushort startAddress)
        {
            using var serialPort = new SerialPort("COM3")
            {
                BaudRate = 9600, DataBits = 8, Parity = Parity.None, StopBits = StopBits.One
            };
            serialPort.Open();

            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

            ushort numRegisters = 1;

            var registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
            
            return Convert.ToInt32(registers[0]);
        }
        
        public int GetHoldingRegisterSignedInt16(ushort startAddress)
        {
            using var serialPort = new SerialPort("COM3")
            {
                BaudRate = 9600, DataBits = 8, Parity = Parity.None, StopBits = StopBits.One
            };
            serialPort.Open();

            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

            ushort numRegisters = 1;

            var registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
            
            // need to check this... not sure my maths is alright
            var converted = Convert.ToInt32(registers[0]);

            return converted - Convert.ToInt32(Math.Pow(2, 15));
        }

        public void SetHoldingRegisterUInt16(ushort startAddress, ushort value)
        {
            using var serialPort = new SerialPort("COM3")
            {
                BaudRate = 9600, DataBits = 8, Parity = Parity.None, StopBits = StopBits.One
            };
            serialPort.Open();

            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

            master.WriteSingleRegister(slaveId, startAddress, value);
        }

        public void SetHoldingRegisterSignedInt16(ushort startAddress, int value)
        {
            using var serialPort = new SerialPort("COM3")
            {
                BaudRate = 9600, DataBits = 8, Parity = Parity.None, StopBits = StopBits.One
            };
            serialPort.Open();

            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

            var signedValue = value + Convert.ToInt32(Math.Pow(2, 15));

            master.WriteSingleRegister(slaveId, startAddress, (ushort)signedValue);
        }
    }
}