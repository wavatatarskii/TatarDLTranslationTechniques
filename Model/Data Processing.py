lines = maybe_download_and_read_file(None, FILENAME)
lines = lines.decode('utf-8')

raw_data = []
for line in lines.split('\n'):
    raw_data.append(line.split('\t'))

print(raw_data[-5:])
# The last element is empty, so omit it
raw_data = raw_data[:-1]


"""## Preprocessing"""



raw_data_tt, raw_data_ru = list(zip(*raw_data))
raw_data_tt = [normalize_string(data) for data in raw_data_tt]
raw_data_ru_in = ['<start> ' + normalize_string(data) for data in raw_data_ru]
raw_data_ru_out = [normalize_string(data) + ' <end>' for data in raw_data_ru]

"""## Tokenization"""

tt_tokenizer = tf.keras.preprocessing.text.Tokenizer(filters='')
tt_tokenizer.fit_on_texts(raw_data_tt)
data_tt = tt_tokenizer.texts_to_sequences(raw_data_tt)
data_tt = tf.keras.preprocessing.sequence.pad_sequences(data_tt,
                                                        padding='post')

ru_tokenizer = tf.keras.preprocessing.text.Tokenizer(filters='')
ru_tokenizer.fit_on_texts(raw_data_ru_in)
ru_tokenizer.fit_on_texts(raw_data_ru_out)
data_ru_in = ru_tokenizer.texts_to_sequences(raw_data_ru_in)
data_ru_in = tf.keras.preprocessing.sequence.pad_sequences(data_ru_in,
                                                           padding='post')

data_ru_out = ru_tokenizer.texts_to_sequences(raw_data_ru_out)
data_ru_out = tf.keras.preprocessing.sequence.pad_sequences(data_ru_out,
                                                            padding='post')

"""## Create tf.data.Dataset object"""

BATCH_SIZE = 64
dataset = tf.data.Dataset.from_tensor_slices(
    (data_tt, data_ru_in, data_ru_out))
dataset = dataset.shuffle(len(data_tt)).batch(BATCH_SIZE)
