class Book:
    id = 0

    def __init__(
        self,
        id,
        year,
        pages,
        price,
        coverType,
        title="Title",
        author="Unknown",
        publisher="Unknown",
    ):
        self.id = id
        self.title = title
        self.author = author
        self.publisher = publisher
        self.year = year
        self.pages = pages
        self.price = price
        self.coverType = coverType
        Book.id += 1

    def id(self):
        return self.id
