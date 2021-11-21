using GalaSoft.MvvmLight;
using System;

namespace ConveyorMaterialAnimationEH.ViewModel
{
    public class Portion : ViewModelBase
    {
        private double _begin;
        private double _length;
        private DateTime _startTime;

        public double Begin { get => _begin; set => Set(ref _begin, value); }
        public double Length { get => _length; set => Set(ref _length, value); }

        public DateTime StartTime { get => _startTime; set => Set(ref _startTime, value); }
    }


}
