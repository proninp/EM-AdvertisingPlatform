# Рекламные площадки

Тестовое задание компании Effective Mobile

## Описание задачи

Рекламодатели часто хотят размещать рекламу в каком-то конкретном регионе (локации),
например только в московской области или только в городе Малые Васюки.

Мы хотим сделать сервис, который помог бы подбирать рекламные площадки для конкретного региона.
Все рекламные площадки перечислены в текстовом файле вместе с локациями, в которых они действуют.

__Пример файла:__
```
Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd
```

Здесь `Ревдинский рабочий` - это рекламная площадка, a `/ru/svrd/revda` и `/ru/svrd/pervik` — локации, в которых действует `Ревдинский рабочий`

Локации вложены, если одна содержит другую как префикс, например `/ru/svrd/ekb` вложена в `/ru/svrd`, `/ru/svrd` вложена в `/ru`, `/ru/svrd/ekb` вложена в `/ru`.
Рекламная площадка действует во всех указанных локациях перечисленных через “,”. Чем меньше вложенность локации, тем глобальнее действует рекламная площадка.

Пример: для локации `/ru/msk` подходят `Газета уральских москвичей` и `Яндекс.Директ`. Для локации `/ru/svrd` подходят `Яндекс.Директ` и `Крутая реклама`, для `/ru/svrd/revda` подходят `Яндекс.Директ`,  `Ревдинский рабочий` и `Крутая реклама`, а для локации `/ru` подходит только `Яндекс.Директ`.

### Что нужно сделать

Необходимо реализовать простой веб сервис, позволяющий хранить и возвращать списки рекламных площадок для заданной локации в запросе. 

<span style="text-decoration:underline">Информация для реализации:</span>

* Веб сервис должен содержать 2 метода REST API:  
    1. Метод загрузки рекламных площадок из файла (должен полностью перезаписывать всю хранимую информацию).
    2. Метод поиска списка рекламных площадок для заданной локации.
* Данные должны храниться строго в оперативной памяти (in-memory collection).
* Важно получать результат поиска рекламных площадок как можно быстрее.
* Считаем, что операция загрузки файла вызывается очень редко, а операция поиска рекламных площадок очень часто.
* Программа не должна ломаться от некорректных входных данных.

Выполненное тестовое задание необходимо опубликовать на GitHub, также необходимо приложить инструкцию по запуску веб сервиса в файле. Юнит тесты приветствуются.

## Запуск

Для запуска сервиса требуется скачать решение и запустить проект API.