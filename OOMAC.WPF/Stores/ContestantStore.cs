﻿using OOMAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMAC.WPF.Stores
{
    public class ContestantStore
    {
        public event Action ContestantStoreChange;

        private List<Contestant> _contestants;
        public List<Contestant> Contestants
        {
            get
            {
                return _contestants;
            }
            set
            {
                _contestants = value;
                ContestantStoreChange?.Invoke();
            }
        }
    }
}
