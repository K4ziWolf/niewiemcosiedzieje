from flask import Flask, request, jsonify
import face_recognition
import base64
from PIL import Image
import io
import numpy as np
import os

app = Flask(__name__)

# Wczytaj znane twarze
known_faces_encodings = []
known_faces_names = []

for file in os.listdir(r"/home/username/Konkurs/Twarze"):
    filepath = os.path.join(r"/home/username/Konkurs/Twarze", file)
    known_image = face_recognition.load_image_file(filepath)
    known_encoding = face_recognition.face_encodings(known_image)
    if len(known_encoding) > 0:  # Sprawdź, czy na obrazie znaleziono twarz
        known_faces_encodings.append(known_encoding[0])
        known_faces_names.append(os.path.splitext(file)[0])  # Zakłada, że nazwa pliku to imię osoby




@app.route('/recognize', methods=['POST'])
def recognize_faces():
  
    try:
        data = request.json
        image_base64 = data['image']

        # Dekoduj obraz w formacie base64
        image_data = base64.b64decode(image_base64)
        image = Image.open(io.BytesIO(image_data))

        # Przekonwertuj obraz PIL na tablicę numpy
        frame = np.array(image)

        # Upewnij się, że obraz jest w formacie RGB
        if frame.shape[-1] == 4:
            # Przekształć RGBA na RGB
            frame = frame[..., :3]

        # Wykryj twarze na obrazie
        face_locations = face_recognition.face_locations(frame)
        frame_encodings = face_recognition.face_encodings(frame, face_locations)

        # Rozpoznaj twarze
        recognized_list = []
        for face_encoding in frame_encodings:
            matches = face_recognition.compare_faces(known_faces_encodings, face_encoding)
            name = "nieznany"

            face_distances = face_recognition.face_distance(known_faces_encodings, face_encoding)
            best_match_index = min(range(len(face_distances)), key=face_distances.__getitem__)
            if matches[best_match_index]:
                name = known_faces_names[best_match_index]

            recognized_list.append(name)

        return jsonify({'recognized_faces': recognized_list})

    except Exception as e:
        print(f"Wyjątek: {e}")  # Rejestrowanie wyjątku
        return jsonify({'error': str(e)})

if __name__ == '__main__':
    app.run(host='0.0.0.0',port=5000)
