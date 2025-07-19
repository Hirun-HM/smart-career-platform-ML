from flask import Flask, request, jsonify
from train_models import predict_career, recommend_courses, analyze_skill_gap

app = Flask(__name__)

@app.route('/predict-career', methods=['POST'])
def predict_career_route():
    data = request.json
    result = predict_career(data)
    return jsonify(result)

@app.route('/recommend-courses', methods=['POST'])
def recommend_courses_route():
    data = request.json
    result = recommend_courses(data)
    return jsonify(result)

@app.route('/analyze-skill-gap', methods=['POST'])
def analyze_skill_gap_route():
    data = request.json
    result = analyze_skill_gap(data)
    return jsonify(result)

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)