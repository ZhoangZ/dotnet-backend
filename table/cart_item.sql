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

 Date: 02/06/2021 08:09:59
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cart_item
-- ----------------------------
DROP TABLE IF EXISTS `cart_item`;
CREATE TABLE `cart_item`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `product_specific_id` bigint(20) NOT NULL,
  `cart_id` bigint(20) NOT NULL,
  `create_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `update_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `deleted` bit(1) NOT NULL DEFAULT b'0',
  `amount` int(255) NOT NULL DEFAULT 0,
  `actived` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `cart_id`(`cart_id`) USING BTREE,
  CONSTRAINT `cart_item_ibfk_1` FOREIGN KEY (`cart_id`) REFERENCES `cart` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 28 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Triggers structure for table cart_item
-- ----------------------------
DROP TRIGGER IF EXISTS `before_insert_cart_item`;
delimiter ;;
CREATE TRIGGER `before_insert_cart_item` BEFORE INSERT ON `cart_item` FOR EACH ROW BEGIN
				SELECT SALE_PRICE into @SALE_PRICE from product_specific where id = new.product_specific_id;
			if new.actived = 1 then
					UPDATE cart set cart.total_price=cart.total_price+ @SALE_PRICE * new.amount, cart.total_item=cart.total_item+new.amount WHERE cart.id=new.cart_id;
				end if;
			
	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table cart_item
-- ----------------------------
DROP TRIGGER IF EXISTS `before_update_cart_item`;
delimiter ;;
CREATE TRIGGER `before_update_cart_item` BEFORE UPDATE ON `cart_item` FOR EACH ROW BEGIN
				SELECT sale_price into @SALE_PRICE from product_specific where id = new.product_specific_id;
				
				if (old.actived = 0 and new.actived = 1) then
					UPDATE cart set cart.total_price=(cart.total_price+ @SALE_PRICE * new.amount) , cart.total_item = cart.total_item+new.amount WHERE cart.id=new.cart_id;
				end if;
				if (old.actived = 1 and new.actived = 0) then
					UPDATE cart set cart.total_price=cart.total_price- @SALE_PRICE * new.amount, cart.total_item=cart.total_item-new.amount WHERE cart.id=new.cart_id;
				end if;
				if (old.actived = 1 and new.actived = 1) then
					UPDATE cart set cart.total_price=cart.total_price+ @SALE_PRICE * (new.amount-old.amount), cart.total_item=cart.total_item+(new.amount-old.amount) WHERE cart.id=new.cart_id;
				end if;

	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table cart_item
-- ----------------------------
DROP TRIGGER IF EXISTS `before_delete_cart_item`;
delimiter ;;
CREATE TRIGGER `before_delete_cart_item` BEFORE DELETE ON `cart_item` FOR EACH ROW BEGIN
					SELECT SALE_PRICE into @SALE_PRICE from product_specific where id = old.product_specific_id;
				if old.actived = 1  then
					UPDATE cart set cart.total_price=cart.total_price- @SALE_PRICE * old.amount, cart.total_item=cart.total_item-old.amount WHERE cart.id=old.cart_id;
				end if;
				

	 END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
