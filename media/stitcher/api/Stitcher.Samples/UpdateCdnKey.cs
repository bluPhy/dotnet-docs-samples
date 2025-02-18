﻿/*
 * Copyright 2022 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// [START videostitcher_update_cdn_key]

using Google.Cloud.Video.Stitcher.V1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

public class UpdateCdnKeySample
{
    public CdnKey UpdateCdnKey(
        string projectId, string location, string cdnKeyId, string hostname,
        string keyName, string privateKey, bool isMediaCdn)
    {
        // Create the client.
        VideoStitcherServiceClient client = VideoStitcherServiceClient.Create();

        CdnKey cdnKey = new CdnKey
        {
            CdnKeyName = CdnKeyName.FromProjectLocationCdnKey(projectId, location, cdnKeyId),
            Hostname = hostname
        };

        string path;
        if (isMediaCdn)
        {
            path = "media_cdn_key";
            cdnKey.MediaCdnKey = new MediaCdnKey
            {
                KeyName = keyName,
                PrivateKey = ByteString.CopyFromUtf8(privateKey)
            };
        }
        else
        {
            path = "google_cdn_key";
            cdnKey.GoogleCdnKey = new GoogleCdnKey
            {
                KeyName = keyName,
                PrivateKey = ByteString.CopyFromUtf8(privateKey)
            };
        }

        UpdateCdnKeyRequest request = new UpdateCdnKeyRequest
        {
            CdnKey = cdnKey,
            UpdateMask = new FieldMask { Paths = { "hostname", path } }
        };

        // Call the API.
        CdnKey newCdnKey = client.UpdateCdnKey(request);

        // Return the result.
        return newCdnKey;
    }
}
// [END videostitcher_update_cdn_key]
