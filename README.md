# TestAlphaBank

Для выполнения тестового задания я использовал .Net Core Web AOI и для клиентской веб фреймворк Angular.
Клиентская часть получала данные взаимодействуся с API посредством HTTP запсросов.
Выбранный мною подход позволяет реализовать клиентскую и серверную часть отдельно друг от друга.

Плюсы:

1) Главным плюсо, конечно же, является выбранный подход;
2) TypeScript - типизированный язык программирования от Microsoft, понятен, удобен;
3) Компонентный подход в Angular;
4) Кросплатформенность .Net Core.

Минусы:

1) Логика работы с сущностью не вынесена на отдельный уровень;
2) Класс обеспечивающий взаимодействие с БД не масштабируемый, т. е. при добавлении новой таблицы, новых свзей и т. д. придется писать все sql запросы с нуля;
3) В клиентской части минусом является то, что я не вынес большую часть логики в сервисы;
4) Так же отнесу к минусу пренебрежение к стратегиям обнаружения;
5) Код не покрыт Unit тестами;
6) Отсутствие ведения истории разработки в системе контроля версиями;
7) Отсутствие кеширования.
