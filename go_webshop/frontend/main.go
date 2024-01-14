package main

import (
	"bytes"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"math/rand"
	"net/http"
	"time"

	"github.com/samber/lo"
)

type product struct {
	Name     string `json:"name"`
	Quantity int    `json:"quantity"`
}
type apiError struct {
	Message string `json:"message"`;
}
var url = "http://localhost:8080";

var products = []string{}

func loop() {
	name := products[rand.Intn(len(products))];
	resp, err := get[product](url + "/products/" + name);

	if (err != nil) {
		fmt.Println(err);
		return;
	}

	if (resp.Quantity <= 0) { 
		fmt.Println("No more " + resp.Name + " left");
		return;
	}

	resp, err = post[*product, product](url + "/products/" + name, new(product));

	if (err != nil) {
		fmt.Println(err);
		return;
	}

	fmt.Println("Bought " + resp.Name, "Quantity left: ", resp.Quantity);
}

func main() {
	data, err := get[[]product](url + "/products");

	if (err != nil) {
		fmt.Println(err);
		return;
	}

	products = lo.Map(*data, func (item product, index int) string { return item.Name});
	amountOfClients := 1;

	for i := 0; i < amountOfClients; i++ {
		go func () {
			for {
				loop();
				sleepAmount := calculateNextSleepAmount();
				time.Sleep(sleepAmount);
			}
		}();
	}
	
}

func calculateNextSleepAmount() time.Duration {
	sleepPercentage := float64(rand.Intn(20)) / 100.0;
	sign := 1;
	if (rand.Intn(2) == 0) {
		sign = -1;
	}
	sleepAmount := float64(time.Second.Milliseconds());
	sleepAmountToAdd := sleepAmount * sleepPercentage * float64(sign);
	return time.Duration(sleepAmount + sleepAmountToAdd) * time.Millisecond;
}

func get[Type any](url string) (*Type, error) {
	resp, _ := http.Get(url);
	if (resp.StatusCode != http.StatusOK) {
		e, _ := readData[apiError](resp);
		return nil, errors.New(e.Message);
	}

	body, err := readData[Type](resp);
	if (err != nil) {
		return nil, errors.New(err.Error());
	}

	return body, nil;
}

func post[Type any, ReturnType any](url string, body Type) (*ReturnType, error) {
	data, err := json.Marshal(body);
	if (err != nil) {
		return nil, errors.New(err.Error());
	}
	responseBody := bytes.NewBuffer(data)

	resp, _ := http.Post(url, "application/json", responseBody);
	
	if (resp.StatusCode != http.StatusOK) {
		e, _ := readData[apiError](resp);
		return nil, errors.New(e.Message);
	}

	result, err := readData[ReturnType](resp);

	if (err != nil) {
		return nil, errors.New(err.Error());
	}
	return result, nil;
}

func readData[Type any](resp *http.Response) (*Type, error) {
	body, err := io.ReadAll(resp.Body)

	if (err != nil) {
		fmt.Println(err);
		return nil, errors.New(err.Error());
	}

	var data = new(Type);
	json.Unmarshal(body, &data)
	return data, nil;
}