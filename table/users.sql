/*
 Navicat Premium Data Transfer

 Source Server         : demo
 Source Server Type    : MySQL
 Source Server Version : 100418
 Source Host           : localhost:3306
 Source Schema         : dotnet

 Target Server Type    : MySQL
 Target Server Version : 100418
 File Encoding         : 65001

 Date: 05/05/2021 19:37:52
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `provider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `confirmed` bit(1) NULL DEFAULT b'0',
  `blocked` bit(1) NULL DEFAULT b'0',
  `active` bit(1) NULL DEFAULT b'0',
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (1, 'test', 'test', 'test', b'0', b'0', b'0', NULL, NULL);
INSERT INTO `users` VALUES (5, 'ongminhdinh', 'ongdinh6@gmail.com', '', b'1', b'1', b'1', '1122', NULL);
INSERT INTO `users` VALUES (6, 'abc@gmail.com', 'abc@gmail.com', NULL, b'0', b'0', b'1', '12312', NULL);
INSERT INTO `users` VALUES (7, 'nguyenvana', 'anguyen@gmail.com', NULL, b'0', b'0', b'0', '827ccb0eea8a706c4c34a16891f84e7b', 'image/avatar/momo.webp');

SET FOREIGN_KEY_CHECKS = 1;
