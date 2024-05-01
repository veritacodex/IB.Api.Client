/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;

namespace IBApi
{
    class BitMask(int p)
    {
        private int m_mask = p;

        public int GetMask()
        {
            return m_mask;
        }

        public void Clear()
        {
            m_mask = 0;
        }

        public bool this[int index]
        {
            get
            {
                return (m_mask & (1 << index)) != 0;
            }
            set
            {
                if (value)
                {
                    m_mask |= 1 << index;
                }
                else
                {
                    m_mask &= ~(1 << index);
                }
            }
        }
    }
}
