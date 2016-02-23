0 0 _ r 1o
0 1 _ r 1i
0 _ _ * accept

1o _ _ l 2o
1o * * r 1o

1i _ _ l 2i
1i * * r 1i

2o 0 _ l 3
2o _ _ * accept
2o * * * reject

2i 1 _ l 3
2i _ _ * accept
2i * * * reject

3 _ _ * accept
3 * * l 4
4 * * l 4
4 _ _ r 0

accept * : r accept2
accept2 * ) * halt