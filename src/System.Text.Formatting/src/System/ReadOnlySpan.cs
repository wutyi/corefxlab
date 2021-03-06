﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace System
{
    // This is a naive implementation of span. We will get a much better one later.
    public struct ReadOnlySpan<T>
    {
        T[] _array;
        int _index;
        int _count;

        public ReadOnlySpan(T[] array, int index, int count)
        {
            _array = array;
            _index = index;
            _count = count;
        }
        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _count;
            }
        }

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _array[_index + index];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T> Slice(int index, int count)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T> Slice(int index)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ReadOnlySpan<T>(T[] from)
        {
            return new ReadOnlySpan<T>(from, 0, from.Length);
        }

        internal T[] CreateArray()
        {
            T[] array = new T[_count];
            var arrayIndex = 0;
            var start = _index;
            var count = _count;
            while(count > 0) {
                array[arrayIndex++] = _array[start++];
                count--;
            }
            return array;
        }
    }
}
