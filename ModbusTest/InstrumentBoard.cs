using System;

namespace ModbusTest
{
    public class InstrumentBoard
    {
        private Modbus modbus;
        
        // input registers
        private ushort tempRegister = 1000;
        private ushort longRegister = 1001;
        private ushort latRegister = 1006;
        private ushort switch1Register = 1011;
        private ushort switch2Register = 1012;
        private ushort switch3Register = 1013;
        
        // holding registers
        private ushort tempOffsetRegister = 2000;
        private ushort ledPower = 2001;
        private ushort stopComms = 2002;
        
        public InstrumentBoard(int slaveId)
        {
            this.modbus = new Modbus(slaveId);
        }

        public double GetTemp()
        {
             var temp =  modbus.GetInputRegisterUInt16(tempRegister);
             return temp / (double) 100;
        }

        public double GetLong()
        {
            var longitude = modbus.GetInputRegisterUInt32(longRegister);
            return longitude / (double) 10000;
        }
        
        public double GetLat()
        {
            var lat = modbus.GetInputRegisterUInt32(latRegister);
            return lat / (double) 10000;
        }

        public bool GetSwitch1()
        {
            var switch1 = modbus.GetInputRegisterUInt16(switch1Register);
            return switch1 == 1;
        }
        
        public bool GetSwitch2()
        {
            var switch2 = modbus.GetInputRegisterUInt16(switch2Register);
            return switch2 == 1;
        }
        
        public bool GetSwitch3()
        {
            var switch3 = modbus.GetInputRegisterUInt16(switch3Register);
            return switch3 == 1;
        }

        public int GetLedPower()
        {
            var led = modbus.GetHoldingRegisterUInt16(ledPower);
            return led;
        }

        public double GetTempOffset()
        {
            var tempOffset = modbus.GetHoldingRegisterSignedInt16(tempOffsetRegister);
            return tempOffset / (double) 100;
        }

        public int GetStopCommsInSeconds()
        {
            var stopTime = modbus.GetHoldingRegisterUInt16(stopComms);
            return stopTime;
        }

        public void SetLedPower(int power)
        {
            if (power < 0 || power > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(power));
            }
            modbus.SetHoldingRegisterUInt16(ledPower, (ushort) power);
        }

        public void SetStopComms(int seconds)
        {
            if (seconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds));
            }
            modbus.SetHoldingRegisterUInt16(stopComms, (ushort) seconds);
        }

        public void SetTempOffSet(double offset)
        {
            var offsetInt = (int)(offset * 100);
            
            modbus.SetHoldingRegisterSignedInt16(tempOffsetRegister, offsetInt);
        }
        
    }
}