import sys
import json
from orchid_pipeline_module import OrchidClassifier, Config

if __name__ == '__main__':
    try:
        image_bytes = sys.stdin.buffer.read()
        classifier = OrchidClassifier(Config())
        result = classifier.classify(image_bytes)
        print(json.dumps(result))
    except Exception as e:
        print(json.dumps({'error': str(e)}))
        sys.exit(1)
