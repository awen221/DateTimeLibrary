using System;

namespace DateTimeLibrary
{
    public class DateTimeLibrary
    {
        abstract public class CheckingCrossDateTime
        {
            protected long PreTicks { set; get; }

            abstract protected long GetCheckTicks(DateTime Now);
            abstract protected long GetNowTicks(DateTime Now);

            public CheckingCrossDateTime()
            {
                PreTicks = DateTime.Now.Ticks;
            }

            public bool Check()
            {
                bool result = false;

                DateTime Now = DateTime.Now;

                long CheckTicks = GetCheckTicks(Now);
                long NowTicks = Now.Ticks;

                if (NowTicks >= CheckTicks && PreTicks < CheckTicks)
                {
                    result = true;
                }

                PreTicks = NowTicks;

                return result;
            }
        }

        /// <summary>
        /// 檢查是否跨越某時某分
        /// </summary>
        public class CheckingCrossTime : CheckingCrossDateTime
        {
            int CheckHour { set; get; }
            int CheckMinute { set; get; }

            public CheckingCrossTime(int checkHour = 0, int checkMinute = 0) : base()
            {
                CheckHour = checkHour;
                CheckMinute = checkMinute;
            }

            protected override long GetCheckTicks(DateTime Now)
            {
                var checkTime = new DateTime(
                    Now.Year, Now.Month, Now.Day, 
                    CheckHour, CheckMinute,0
                    );

                return checkTime.Ticks;
            }

            protected override long GetNowTicks(DateTime Now)
            {
                return DateTime.Now.Ticks;
            }
        }
    }
}
