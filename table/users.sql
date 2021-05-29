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

 Date: 29/05/2021 20:53:46
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
  `fullname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `phone` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `provider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `confirmed` bit(1) NULL DEFAULT b'0',
  `blocked` bit(1) NULL DEFAULT b'0',
  `active` bit(1) NULL DEFAULT b'0',
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `opt` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (1, 'test', 'test', NULL, NULL, NULL, 'test', b'0', b'0', b'0', NULL, NULL, NULL);
INSERT INTO `users` VALUES (5, 'ongminhdinh', 'ongdinh6@gmail.com', NULL, NULL, NULL, '', b'1', b'1', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (6, 'abc@gmail.com', 'abc@gmail.com', NULL, NULL, NULL, NULL, b'0', b'0', b'1', '12312', NULL, NULL);
INSERT INTO `users` VALUES (7, 'nguyenvana', 'anguyen@gmail.com', NULL, NULL, NULL, NULL, b'0', b'0', b'0', '827ccb0eea8a706c4c34a16891f84e7b', 'image/avatar/momo.webp', NULL);
INSERT INTO `users` VALUES (11, 'diemdiem', 'dinhdinh@gmail.com', 'Trần Quang Diệm', '0988766567', 'Dong Nai', NULL, b'0', b'0', b'0', NULL, NULL, NULL);
INSERT INTO `users` VALUES (12, 'dinh2', 'dinh2@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'0', b'1', '****', NULL, NULL);
INSERT INTO `users` VALUES (13, 'dinh3', 'dinh3@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'0', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (14, 'dinh5', 'dinh5@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'0', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (15, 'dinh6', 'dinh6@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'0', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (16, 'diemdiem', 'dinh7@gmail.com', 'Trần Quang Diệm', '0988766567', 'Dong Nai', NULL, b'0', b'0', b'0', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (17, 'hoanghoang', 'dinh8@gmail.com', 'Lê Tấn Hoàng', '0988766567', 'Dong Nai', NULL, b'0', b'0', b'0', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (18, 'letanhoang', 'dinh10@gmail.com', 'Lê Tấn Hoàng', '0988766567', 'Đồng Nai', NULL, b'0', b'0', b'0', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (19, 'dinh11', 'dinh11@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'0', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
