/*
 Navicat Premium Data Transfer

 Source Server         : mysql_demo
 Source Server Type    : MySQL
 Source Server Version : 100418
 Source Host           : localhost:3306
 Source Schema         : dotnet

 Target Server Type    : MySQL
 Target Server Version : 100418
 File Encoding         : 65001

 Date: 06/09/2021 17:33:36
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for comment
-- ----------------------------
DROP TABLE IF EXISTS `comment`;
CREATE TABLE `comment`  (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `user_id` int(10) NOT NULL,
  `order_id` bigint(20) NOT NULL,
  `product_id` int(255) NOT NULL,
  `rate` double(1, 0) NOT NULL DEFAULT 0,
  `content` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `created_date` date NOT NULL DEFAULT utc_timestamp,
  `active` int(255) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `user_id`(`user_id`) USING BTREE,
  INDEX `product_id`(`product_id`) USING BTREE,
  INDEX `order_id`(`order_id`) USING BTREE,
  CONSTRAINT `comment_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `product_2` (`ID`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `comment_ibfk_2` FOREIGN KEY (`order_id`) REFERENCES `order` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 30 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of comment
-- ----------------------------
INSERT INTO `comment` VALUES (27, 27, 61, 9, 4, 'San pham 9 don hang 61 tot [mang]', '2021-09-06', 1);
INSERT INTO `comment` VALUES (28, 27, 67, 1, 4, 'San pham 1 don hang 67 tot', '2021-09-06', 1);
INSERT INTO `comment` VALUES (29, 27, 61, 7, 4, 'San pham 7 don hang 61 tot', '2021-09-06', 1);

-- ----------------------------
-- Triggers structure for table comment
-- ----------------------------
DROP TRIGGER IF EXISTS `after_insert_comment`;
delimiter ;;
CREATE TRIGGER `after_insert_comment` AFTER INSERT ON `comment` FOR EACH ROW BEGIN
				
				UPDATE `product_2` set rating=rating+1 where id =new.product_id;
				UPDATE `product_2` set sum_rating=sum_rating+new.rate where id =new.product_id;
	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table comment
-- ----------------------------
DROP TRIGGER IF EXISTS `after_update_comment`;
delimiter ;;
CREATE TRIGGER `after_update_comment` AFTER UPDATE ON `comment` FOR EACH ROW BEGIN
				
				UPDATE `product_2` set sum_rating=sum_rating-old.rate+new.rate where id =old.product_id;
	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table comment
-- ----------------------------
DROP TRIGGER IF EXISTS `after_delete_comment`;
delimiter ;;
CREATE TRIGGER `after_delete_comment` AFTER DELETE ON `comment` FOR EACH ROW BEGIN
				
				UPDATE `product_2` set rating=rating-1 where id =old.product_id;
				UPDATE `product_2` set sum_rating=sum_rating-old.rate where id =old.product_id;
	 END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
