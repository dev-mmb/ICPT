package main

import (
	"fmt"
	"math/rand"
	"net/http"
	"slices"
	"sync"
	"time"

	"github.com/gin-gonic/gin"
)

type product struct {
	Name        string  `json:"name"`
	Quantity 	int     `json:"quantity"`
}

var products = []product{
	{Name: "Laptop", Quantity: 10},
	{Name: "Mouse", Quantity: 10},
	{Name: "Keyboard", Quantity: 10},
}
var productsMutex sync.Mutex;

func serverLoop() {
	productsMutex.Lock();

	index := rand.Intn(len(products));
	products[index].Quantity += 1;

	productsMutex.Unlock();
	time.Sleep(1 * time.Second);
}

func getProducts(c *gin.Context) {
	productsMutex.Lock();
	productsCopy := make([]product, len(products));
	copy(productsCopy, products);
	productsMutex.Unlock();

	c.JSON(http.StatusOK, productsCopy);
}

func getProductByParamName(c *gin.Context) {
	name := c.Param("name");
	productsMutex.Lock();

	productIndex := slices.IndexFunc(products, func(p product) bool {
		return p.Name == name;
	});

	if (productIndex == -1) {
		productsMutex.Unlock();
		c.JSON(http.StatusNotFound, gin.H{"message": "Product not found"});
		return;
	}

	productsMutex.Unlock();

	c.JSON(http.StatusOK, products[productIndex]);
}

func buyProduct(c *gin.Context) {
	name := c.Param("name");
	fmt.Println("name: ", name);

	productsMutex.Lock();

	productIndex := slices.IndexFunc(products, func(p product) bool {
		return p.Name == name;
	});

	if (productIndex == -1) {
		productsMutex.Unlock();
		c.JSON(http.StatusNotFound, gin.H{"message": "Product not found"});
		return;
	}
	quantity := products[productIndex].Quantity;
	if (quantity < 1) {
		productsMutex.Unlock();
		c.JSON(http.StatusNotFound, gin.H{"message": "Product out of stock"});
		return;
	}
	
	products[productIndex].Quantity -= 1;
	productsMutex.Unlock();

	c.JSON(http.StatusOK, products[productIndex]);
}

func main() {
	go func() {
		for {
			serverLoop();
		}
	}();

	router := gin.Default();
	router.GET("/products", getProducts);
	router.GET("/products/:name", getProductByParamName);
	router.POST("/products/:name", buyProduct);
	router.Run(":8080");
}