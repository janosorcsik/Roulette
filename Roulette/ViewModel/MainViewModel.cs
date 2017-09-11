using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Roulette.ViewModel
{
    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Commands

        private RelayCommand _addCommand;

        public RelayCommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(Add));

        private RelayCommand _clearCommand;

        public RelayCommand ClearCommand => _clearCommand ?? (_clearCommand = new RelayCommand(Clear));

        #endregion

        #region Functions

        private void Add()
        {
            if (Numbers == null)
            {
                Clear();
            }

            if (Number > -1 && Number < 37)
            {
                Numbers.Add(Number);
            }

            NumberCount = Numbers.Count;

            Calculator();
        }

        private void Clear()
        {
            Number = 0;

            Numbers = new ObservableCollection<int>();

            NumberCount = Numbers.Count;

            Calculator();
        }

        private void Calculator()
        {
            var firstSigned = -1;
            var holeNumber = 8;
            var lastNumber = RouletteTable.LastOrDefault();

            var holes = new List<List<int>>();
            var hole = new List<int>();

            foreach (var r in RouletteTable)
            {
                if (!Numbers.Contains(r))
                {
                    hole.Add(r);
                }
                else
                {
                    if (firstSigned == -1)
                    {
                        firstSigned = r;
                    }
                    holes.Add(hole);
                    hole = new List<int>();
                }

                if (r == lastNumber)
                {
                    foreach (var r2 in RouletteTable)
                        if (r2 == firstSigned)
                        {
                            holes.Add(hole);
                            break;
                        }
                        else
                        {
                            hole.Add(r2);
                        }
                }
            }

            var sb = new StringBuilder();

            foreach (var hol in holes.Where(i => i.Count >= holeNumber))
            {
                foreach (var h in hol)
                {
                    sb.Append(h).Append(", ");
                }
                sb.AppendLine();
            }

            TipText = sb.ToString();

            sb = new StringBuilder();

            foreach (var n in Numbers)
            {
                    sb.Append(n).Append(", ");
            }

            NumberText = sb.ToString();

        }

        #endregion

        private string _tipText;

        public string TipText
        {
            get { return _tipText; }
            set
            {
                if (_tipText != value)
                {
                    _tipText = value;
                    RaisePropertyChanged(nameof(TipText));
                }
            }
        }

        private string _numberText;

        public string NumberText
        {
            get { return _numberText; }
            set
            {
                if (_numberText != value)
                {
                    _numberText = value;
                    RaisePropertyChanged(nameof(NumberText));
                }
            }
        }

        private ObservableCollection<int> _numbers;

        public ObservableCollection<int> Numbers
        {
            get { return _numbers; }
            set
            {
                if (_numbers != value)
                {
                    _numbers = value;
                    RaisePropertyChanged(nameof(Numbers));
                }
            }
        }


        private int _numberCount;

        public int NumberCount
        {
            get { return _numberCount; }
            set
            {
                if (_numberCount != value)
                {
                    _numberCount = value;
                    RaisePropertyChanged(nameof(NumberCount));
                }
            }
        }

        private int _number;

        public int Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    RaisePropertyChanged(nameof(Number));
                }
            }
        }

        private static readonly List<int> RouletteTable = new List<int>
        {
            0,
            32,
            15,
            19,
            4,
            21,
            2,
            25,
            17,
            34,
            6,
            27,
            13,
            36,
            11,
            30,
            8,
            23,
            10,
            5,
            24,
            16,
            33,
            1,
            20,
            14,
            31,
            9,
            22,
            18,
            29,
            7,
            28,
            12,
            35,
            3,
            26
        };

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;

                switch (columnName)
                {
                    case "Number":
                        if (Number < 0)
                        {
                            errorMessage = "Nem lehet kisebb, mint 0!";
                        }
                        else
                        {
                            if (Number > 36)
                            {
                                errorMessage = "Nem lehet nagyobb, mint 36!";
                            }
                        }
                        break;
                }

                return errorMessage;
            }
        }

        public string Error
        {
            get { return string.Empty; }
        }

        #endregion
    }
}
