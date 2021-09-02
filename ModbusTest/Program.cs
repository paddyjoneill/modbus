using System;

namespace ModbusTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var instrumentBoard = new InstrumentBoard(1);

            var temp = instrumentBoard.GetTemp();
            var longitude = instrumentBoard.GetLong();
            var latitude = instrumentBoard.GetLat();
            var switch1 = instrumentBoard.GetSwitch1();
            var switch2 = instrumentBoard.GetSwitch2();
            var switch3 = instrumentBoard.GetSwitch3();

            var ledPower = instrumentBoard.GetLedPower();
            var tempOffset = instrumentBoard.GetTempOffset();
            var stopComms = instrumentBoard.GetStopCommsInSeconds();

            Console.WriteLine($"Temp is {temp}");
            Console.WriteLine($"Longitude is {longitude}");
            Console.WriteLine($"Latitude is {latitude}");
            Console.WriteLine($"Switch 1 is {switch1}");
            Console.WriteLine($"Switch 2 is {switch2}");
            Console.WriteLine($"Switch 3 is {switch3}");
            Console.WriteLine($"LED Power is {ledPower}");
            Console.WriteLine($"Temp Offset is {tempOffset}");
            Console.WriteLine($"Stop Comms is {stopComms}");
            
            // test setting holding registers
            instrumentBoard.SetLedPower(50);
            ledPower = instrumentBoard.GetLedPower();
            Console.WriteLine($"LED Power is {ledPower}");
            
            instrumentBoard.SetStopComms(2);
            stopComms = instrumentBoard.GetStopCommsInSeconds();
            Console.WriteLine($"Stop Comms is {stopComms}");
            
            
            instrumentBoard.SetTempOffSet(1.23);
            tempOffset = instrumentBoard.GetTempOffset();
            Console.WriteLine($"Temp Offset is {tempOffset}");
        }


    }
}