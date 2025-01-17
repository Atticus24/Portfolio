import cv2 # read image / camera input
from pyzbar.pyzbar import decode


cap = cv2.VideoCapture(0)
cap.set(3, 640) # 3 -width
cap.set(4, 480) # 4 -Height

camera = True
while camera == True:
    frame = cap.read()

    for code in decode(frame):
        print(code.type)
        print(code.data.decode)

    cv2.imshow("scan",frame)
    cv2.waitKey(0)
    cv2.destroyAllWindows()


    