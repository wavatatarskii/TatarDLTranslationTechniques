!git clone https://github.com/wavatatarskii/TatarDLTranslationTechniques.git

import tensorflow as tf
import numpy as np
import unicodedata
import re
import os
import requests
from zipfile import ZipFile
import time
tf.compat.v1.enable_eager_execution()

# Mode can be either 'train' or 'infer'
# Set to 'infer' will skip the training
MODE = 'train'
FILENAME = '/content/TatarDLTranslationTechniques/tatTABru60000-1.zip'

def maybe_download_and_read_file(url, filename):
    """ Download and unzip training data
    Args:
        url: data url
        filename: zip filename
    
    Returns:
        Training data: an array containing text lines from the data
    """
    if not os.path.exists(filename):
        session = requests.Session()
        response = session.get(url, stream=True)

        CHUNK_SIZE = 32768
        with open(filename, "wb") as f:
            for chunk in response.iter_content(CHUNK_SIZE):
                if chunk:
                    f.write(chunk)

    zipf = ZipFile(filename)
    filename = zipf.namelist()
    with zipf.open(filename[0]) as f:
        lines = f.read()

    return lines






def normalize_string(s):
    s = re.sub(r'([!.?])', r' \1', s)
    s = re.sub(r'\s+', r' ', s)
    return s
