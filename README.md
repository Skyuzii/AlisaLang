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
	let pinCode = console.input("Введите пинкод: ")
	let resultMsg = checkPinCode(pinCode)
	console.print(resultMsg)
}

func checkPinCode(pinCode) {
	if pinCode == "2482" {
		return "Вы победили!"
	}
	
	return "Что-то тут не так"
}

```
