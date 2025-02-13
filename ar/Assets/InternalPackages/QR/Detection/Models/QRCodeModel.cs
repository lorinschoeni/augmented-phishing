using Microsoft.MixedReality.OpenXR;
using Microsoft.MixedReality.QR;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using UnityEngine;

namespace PhishAR.QR.Detection.Models
{
    public class QRCodeModel
    {
        public Guid Id { get; }
        public string Payload { get; }
        public float PhysicalLength { get; }
        public Pose TopLeftPose { get; }
        public Pose CenterPose { get; }
        public DateTimeOffset LastDetectedTime { get; }

        public QRCodeModel(QRCode qrCode) : this(qrCode.Id, qrCode.Data, qrCode.PhysicalSideLength,
            qrCode.SpatialGraphNodeId, qrCode.LastDetectedTime, qrCode.SystemRelativeLastDetectedTime)
        { }

        private QRCodeModel(Guid id, string payload, float length, Guid spatialGraphNodeId,
            DateTimeOffset lastDetectedTime, TimeSpan systemRelativeLastDetectedTime)
        {
            Id = id;
            Payload = payload;
            PhysicalLength = length;
            LastDetectedTime = lastDetectedTime;

            Pose qrCodeTopLeft = LocateQRCodeSpatialNode(spatialGraphNodeId, systemRelativeLastDetectedTime);
            TopLeftPose = new Pose(qrCodeTopLeft.position, qrCodeTopLeft.rotation * Quaternion.Euler(180, 0, 0));
            CenterPose = CalculateCenterOfQRCode(TopLeftPose, PhysicalLength);
        }

        private Pose LocateQRCodeSpatialNode(Guid spatialGraphNodeId, TimeSpan timeSpan)
        {
            SpatialGraphNode qrCodeSpatialGraphNode = (spatialGraphNodeId != Guid.Empty)
                ? SpatialGraphNode.FromStaticNodeId(spatialGraphNodeId)
                : null;

            Pose qrCodeTopLeft = Pose.identity;
            if (qrCodeSpatialGraphNode != null)
            {
                bool abilityToLocate = qrCodeSpatialGraphNode.TryLocate(timeSpan.Ticks, out qrCodeTopLeft);
                if (!abilityToLocate) return Pose.identity;

                if (IsUsingTeleportation(abilityToLocate)) qrCodeTopLeft = qrCodeTopLeft.GetTransformedBy(CameraCache.Main.transform.parent);
            }
            return qrCodeTopLeft;
        }

        private bool IsUsingTeleportation(bool abilityToLocate)
        {
            return abilityToLocate && CameraCache.Main.transform.parent != null;
        }

        private Pose CalculateCenterOfQRCode(Pose qrCodeTopLeft, float physicalLength)
        {
            float midLength = physicalLength * 0.5f;
            Pose qrCodeCenter = new Pose(qrCodeTopLeft.position, qrCodeTopLeft.rotation);
            qrCodeCenter.position += qrCodeTopLeft.rotation * (midLength * Vector3.right) + qrCodeTopLeft.rotation * (midLength * Vector3.down);
            return qrCodeCenter;
        }
    }
}
