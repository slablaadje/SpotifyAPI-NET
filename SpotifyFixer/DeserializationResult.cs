using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyFixer
{
    public enum DeserializationResult
    {
        CantDeserializeSavedTracks,
        NoFile,
        Failed,
        UpToDate,
        Outdated,
        OutdatedMoreTracks,
        OutdatedLessTracks,
    }
}
