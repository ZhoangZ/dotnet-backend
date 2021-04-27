/*
 Navicat Premium Data Transfer

 Source Server         : My SQL
 Source Server Type    : MySQL
 Source Server Version : 100417
 Source Host           : localhost:3306
 Source Schema         : dotnet

 Target Server Type    : MySQL
 Target Server Version : 100417
 File Encoding         : 65001

 Date: 27/04/2021 12:42:00
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for brand
-- ----------------------------
DROP TABLE IF EXISTS `brand`;
CREATE TABLE `brand`  (
  `id` int(2) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `logo` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `actived` bit(1) NULL DEFAULT b'0',
  `deleted` bit(1) NULL DEFAULT b'0',
  `amount` int(255) NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 39 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of brand
-- ----------------------------
INSERT INTO `brand` VALUES (1, 'SAMSUNG', 'img/brand/samsung.jpg', b'1', b'0', 0);
INSERT INTO `brand` VALUES (2, 'XIAOMI', 'img/brand/xiaomi.png', b'1', b'0', 0);
INSERT INTO `brand` VALUES (3, 'HUAWEI', 'img/brand/huawei.jpg', b'1', b'0', 0);
INSERT INTO `brand` VALUES (4, 'REALME', 'img/brand/realme.png', b'0', b'0', 0);
INSERT INTO `brand` VALUES (5, 'OPPO', 'img/brand/oppo.png', b'1', b'0', 0);
INSERT INTO `brand` VALUES (6, 'VSMART', 'img/brand/vsmart.png', b'1', b'0', 0);
INSERT INTO `brand` VALUES (7, 'VIVO', 'img/brand/vivo.jpg', b'0', b'0', 0);
INSERT INTO `brand` VALUES (8, 'APPLE', 'brand/Apple_logo_black.svg.png', b'1', b'0', 0);
INSERT INTO `brand` VALUES (9, 'NOKIA', 'brand/Apple_logo_black.svg.png', b'1', b'0', 0);
INSERT INTO `brand` VALUES (10, 'MEIZU', 'brand/Apple_logo_black.svg.png', b'0', b'0', 0);
INSERT INTO `brand` VALUES (11, 'WIKO', 'brand/Apple_logo_black.svg.png', b'0', b'0', 0);
INSERT INTO `brand` VALUES (12, 'REALME', 'brand/Apple_logo_black.svg.png', b'0', b'0', 0);
INSERT INTO `brand` VALUES (13, 'ULEFONE', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (14, 'NEFFOS', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (15, 'ITEL', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (16, 'MASSTEL', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (17, 'HONOR', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (18, 'COOLPAD', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (19, 'BLACKBERRY', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (20, 'ASUS', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (21, 'OUKITEL', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (22, 'SYMPHONY', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (23, 'HYUNDAI', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (24, 'BPHONE', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (25, 'M-HORSE', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (26, 'BLUBOO', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (27, 'MOBELL', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (28, 'MOBIISTAR', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (29, 'NONY', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (30, 'VIVAS', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (31, 'FPT', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (32, 'BLACK SHARK', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (33, 'INFINIX', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (34, 'PHILIPS', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (35, 'POCOPHONE BY XIAOMI', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (36, 'SHARP', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (37, 'SUNTEK', NULL, b'0', b'0', 0);
INSERT INTO `brand` VALUES (38, 'TECNO', NULL, b'0', b'0', 0);

SET FOREIGN_KEY_CHECKS = 1;
