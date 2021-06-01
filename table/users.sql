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

 Date: 01/06/2021 00:14:06
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `fullname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `phone` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `provider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `blocked` bit(1) NULL DEFAULT b'0',
  `active` bit(1) NULL DEFAULT b'1',
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `opt` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (1, 'test', NULL, NULL, NULL, 'test', b'0', b'0', NULL, NULL, NULL);
INSERT INTO `users` VALUES (5, 'ongdinh6@gmail.com', NULL, NULL, NULL, '', b'1', b'1', 'A8AE104615CB4E966DDB435F3E575A02', NULL, NULL);
INSERT INTO `users` VALUES (6, 'abc@gmail.com', NULL, NULL, NULL, NULL, b'0', b'1', '12312', NULL, NULL);
INSERT INTO `users` VALUES (7, 'anguyen@gmail.com', NULL, NULL, NULL, NULL, b'0', b'0', '827ccb0eea8a706c4c34a16891f84e7b', 'image/avatar/momo.webp', NULL);
INSERT INTO `users` VALUES (11, 'dinhdinh@gmail.com', 'Trần Quang Diệm', '0988766567', 'Dong Nai', NULL, b'0', b'0', NULL, NULL, NULL);
INSERT INTO `users` VALUES (12, 'dinh2@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'1', '****', NULL, NULL);
INSERT INTO `users` VALUES (13, 'dinh3@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (14, 'dinh5@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (15, 'dinh6@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (16, 'dinh7@gmail.com', 'Trần Quang Diệm', '0988766567', 'Dong Nai', NULL, b'0', b'0', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (17, 'dinh8@gmail.com', 'Lê Tấn Hoàng', '0988766567', 'Dong Nai', NULL, b'0', b'0', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (18, 'dinh10@gmail.com', 'Lê Tấn Hoàng', '0988766567', 'Đồng Nai', NULL, b'0', b'0', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (19, 'dinh11@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'1', 'F59BD65F7EDAFB087A81D4DCA06C4910', NULL, NULL);
INSERT INTO `users` VALUES (20, 'dinh20@gmail.com', 'Lê Tấn Hoàng', '0988766567', 'Đồng Nai', NULL, b'0', b'0', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (21, 'dinh12@gmail.com', 'Ông Minh Đình', '0988766567', 'HCM', NULL, b'0', b'1', '827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL);
INSERT INTO `users` VALUES (25, 'tanhoang99.999@gmail.com', 'Tấn Hoàng', '0399115950', 'Bình Dương', NULL, b'0', b'1', '0738D0A6141D4E17DB5FCADB94EE0D7A', NULL, NULL);

-- ----------------------------
-- Triggers structure for table users
-- ----------------------------
DROP TRIGGER IF EXISTS `after_insert_users`;
delimiter ;;
CREATE TRIGGER `after_insert_users` AFTER INSERT ON `users` FOR EACH ROW BEGIN
				INSERT INTO cart ( user_id, total_item,total_price) VALUES (new.id, 0,0);
				

	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table users
-- ----------------------------
DROP TRIGGER IF EXISTS `before_delete_users`;
delimiter ;;
CREATE TRIGGER `before_delete_users` BEFORE DELETE ON `users` FOR EACH ROW BEGIN
				DELETE FROM user_role WHERE users_id=old.id;
				DELETE FROM cart WHERE user_id=old.id;
				

	 END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
