#include <stdio.h>
#include <stdlib.h>
#include <math.h>

char getRandomChar() {
	int c = rand() % 26;
	return (char) 'a' + c;
}

void printRandomChars(int length) {
	char store[10];
	for (int i = 0; i < length; i++) {
		do {
			char c = getRandomChar();
			
			int isSame = 0;
			for (int j = 0; j < i; j++) if (c == store[j]) isSame = 1;
			if (isSame) continue;
			
			printf("%c", c);
			store[i] = c;
			break;
		} while(1);
	}
}

int main(int argc, char** argv) {
	if (argc < 2) return 1;
	int size = strtol(argv[1], NULL, 10);
	srand((unsigned) time(0));
	
	int s = pow(10.0, size);
	for (int p = 0; p < s; p++) {
		int n = rand() % 7 + 1;
		printRandomChars(n);
		printf("\n");
	}
	return 0;
}