from ultralytics import YOLO
from PIL import Image
from io import BytesIO
import json

class Config:
    STAGE_MODEL_PATH = './models/stage_model.pt'
    DISEASE_MODEL_PATH = './models/disease_model.pt'
    IMG_SIZE = 224

class OrchidClassifier:
    def __init__(self, config: Config):
        self.stage_model = YOLO(config.STAGE_MODEL_PATH)
        self.disease_model = YOLO(config.DISEASE_MODEL_PATH)

    def classify(self, image_bytes: bytes) -> dict:
        img = Image.open(BytesIO(image_bytes)).convert('RGB')

        stage_result = self.stage_model.predict(img, imgsz=Config.IMG_SIZE, verbose=False)
        stage_class = stage_result[0].probs.top1
        stage_label = self.stage_model.model.names[stage_class]

        disease_result = self.disease_model.predict(img, imgsz=Config.IMG_SIZE, verbose=False)
        disease_class = disease_result[0].probs.top1
        disease_label = self.disease_model.model.names[disease_class]

        return {
            'stage': stage_label,
            'disease': disease_label
        }

