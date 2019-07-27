﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSDevice {
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceModel model;
        public string framePath;
        public double frameScale;
        public SSPosition? position;
        public AlignDevice alignX;
        public AlignDevice alignY;
        public SSPosition? screenOffset;
        public double? rotation;
        public SSSize screenSize;
        public string screenshotPath;
        public bool? rightPart;

        public void setOffset() {
            switch (model) {
                case DeviceModel.iPhoneXsMax:
                    screenOffset = new SSPosition(140, 140);
                    break;
                case DeviceModel.iPhone8Plus:
                    screenOffset = new SSPosition(200, 400);
                    break;
                default:
                    break;
            }
        }
        
        public SSPosition getPosition(int deviceWidth, int deviceHeight, int canvasWidth, int canvasHeight) {
            var calculatedPos = new SSPosition(this.position?.x ?? 0, this.position?.y ?? 0);
            if (alignX != null) {
                if (alignX.style == AlignKeys.center) {
                    calculatedPos.x = (canvasWidth - deviceWidth) / 2;
                } else if (alignX.style == AlignKeys.leftSpace) {
                    calculatedPos.x = alignX.value;
                } else if (alignX.style == AlignKeys.rightSpace) {
                    calculatedPos.x = canvasWidth - deviceWidth - alignX.value;
                }
            }
            if (alignY != null) {
                if (alignY.style == AlignKeys.center) {
                    calculatedPos.y = (canvasHeight - deviceHeight) / 2;
                } else if (alignY.style == AlignKeys.topSpace) {
                    calculatedPos.y = alignY.value;
                } else if (alignY.style == AlignKeys.bottomSpace) {
                    calculatedPos.y = canvasHeight - deviceHeight - alignY.value;
                }
            }
            return calculatedPos;
        }
    }

    public enum AlignKeys {
        leftSpace,
        rightSpace,
        topSpace,
        bottomSpace,
        center
    }

    public class AlignDevice {
        [JsonConverter(typeof(StringEnumConverter))]
        public AlignKeys style = AlignKeys.center;
        public int value = 0;
    }

    public enum DeviceModel {
        iPhoneXsMax,
        iPhone8Plus
    }
}
