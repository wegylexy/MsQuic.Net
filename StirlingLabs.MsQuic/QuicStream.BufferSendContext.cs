using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Quic;
using StirlingLabs.Native;

namespace StirlingLabs.MsQuic;

public partial class QuicStream
{
    [PublicAPI]
    private sealed class BufferSendContext : SendContext
    {
        public readonly unsafe QUIC_BUFFER* QuicBuffer;

        public unsafe BufferSendContext(QUIC_BUFFER* quicBuffer, TaskCompletionSource<bool> taskCompletionSource)
            : base(taskCompletionSource)
        {
            QuicBuffer = quicBuffer;
        }

        public override unsafe void Dispose()
            => NativeMemory.Free(QuicBuffer);
    }
}
