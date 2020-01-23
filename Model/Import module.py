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
