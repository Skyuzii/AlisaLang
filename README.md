# AlisaLang

### Структура
Программа обязательно должна содержать метод `main`. Он является входной точкой программы.
``` 
func main() {
  // logic
}
```

### Объявление переменной
Язык не содержит типы. Чтобы объявить переменную, вы должны написать ключевое слово `let`.
```
let foo = "bar"
foo = "foobar"
```

### Условия
Скобки можно опустить
``` 
if condition {

} elif condition {

} else {

}
```

### Циклы
Язык поддерживает ключевые слова `continue` и `break`.
```
while condition {

}
```

### Объявление метода
Чтобы объявить метод, вы должна написать ключевое слово `func`.
```
func sum(firstNumber, secondNumber) {
  return firstNumber + secondNumber
}
```

### Системные методы
`console.print(text)` - вывести на консоль<br>
`console.input(text)` - вывести на консоль, получить ответ от пользователя

### Пример простой программы
```
func main() {
	while true {
		let pinCode = console.input("Введите пинкод: ")
		let result = checkPinCode(pinCode)
		if result == true {
			console.print("Вы победили!")
			break
		}
		
		console.print("Что-то не так, попробуйте еще раз")
	}
}

func checkPinCode(pinCode) {
	if pinCode == "2482" {
		return true
	}
	
	return false
}

```
