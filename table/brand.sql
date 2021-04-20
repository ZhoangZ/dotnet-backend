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

 Date: 21/04/2021 01:05:04
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
  `active` bit(1) NULL DEFAULT b'1',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 39 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of brand
-- ----------------------------
INSERT INTO `brand` VALUES (1, 'SamSung', 'img/brand/samsung.jpg', b'1');
INSERT INTO `brand` VALUES (2, 'Xiaomi', 'img/brand/xiaomi.png', b'1');
INSERT INTO `brand` VALUES (3, 'Huawei', 'img/brand/huawei.jpg', b'1');
INSERT INTO `brand` VALUES (4, 'Realme', 'img/brand/realme.png', b'1');
INSERT INTO `brand` VALUES (5, 'Oppo', 'img/brand/oppo.png', b'1');
INSERT INTO `brand` VALUES (6, 'VSmart', 'img/brand/vsmart.png', b'1');
INSERT INTO `brand` VALUES (7, 'Vivo', 'img/brand/vivo.jpg', b'1');
INSERT INTO `brand` VALUES (8, 'Apple', 'brand/Apple_logo_black.svg.png', b'1');
INSERT INTO `brand` VALUES (9, 'Nokia', 'brand/Apple_logo_black.svg.png', b'1');
INSERT INTO `brand` VALUES (10, 'Meizu', 'brand/Apple_logo_black.svg.png', b'1');
INSERT INTO `brand` VALUES (11, 'Wiko', 'brand/Apple_logo_black.svg.png', b'1');
INSERT INTO `brand` VALUES (12, 'Realme', 'brand/Apple_logo_black.svg.png', b'1');
INSERT INTO `brand` VALUES (13, 'ULEFONE', NULL, b'1');
INSERT INTO `brand` VALUES (14, 'NEFFOS', NULL, b'1');
INSERT INTO `brand` VALUES (15, 'ITEL', NULL, b'1');
INSERT INTO `brand` VALUES (16, 'MASSTEL', NULL, b'1');
INSERT INTO `brand` VALUES (17, 'HONOR', NULL, b'1');
INSERT INTO `brand` VALUES (18, 'COOLPAD', NULL, b'1');
INSERT INTO `brand` VALUES (19, 'BLACKBERRY', NULL, b'1');
INSERT INTO `brand` VALUES (20, 'ASUS', NULL, b'1');
INSERT INTO `brand` VALUES (21, 'OUKITEL', NULL, b'1');
INSERT INTO `brand` VALUES (22, 'SYMPHONY', NULL, b'1');
INSERT INTO `brand` VALUES (23, 'HYUNDAI', NULL, b'1');
INSERT INTO `brand` VALUES (24, 'BPHONE', NULL, b'1');
INSERT INTO `brand` VALUES (25, 'M-HORSE', NULL, b'1');
INSERT INTO `brand` VALUES (26, 'BLUBOO', NULL, b'1');
INSERT INTO `brand` VALUES (27, 'MOBELL', NULL, b'1');
INSERT INTO `brand` VALUES (28, 'MOBIISTAR', NULL, b'1');
INSERT INTO `brand` VALUES (29, 'NONY', NULL, b'1');
INSERT INTO `brand` VALUES (30, 'VIVAS', NULL, b'1');
INSERT INTO `brand` VALUES (31, 'FPT', NULL, b'1');
INSERT INTO `brand` VALUES (32, 'BLACK SHARK', NULL, b'1');
INSERT INTO `brand` VALUES (33, 'INFINIX', NULL, b'1');
INSERT INTO `brand` VALUES (34, 'PHILIPS', NULL, b'1');
INSERT INTO `brand` VALUES (35, 'POCOPHONE BY XIAOMI', NULL, b'1');
INSERT INTO `brand` VALUES (36, 'SHARP', NULL, b'1');
INSERT INTO `brand` VALUES (37, 'SUNTEK', NULL, b'1');
INSERT INTO `brand` VALUES (38, 'TECNO', NULL, b'1');

SET FOREIGN_KEY_CHECKS = 1;
