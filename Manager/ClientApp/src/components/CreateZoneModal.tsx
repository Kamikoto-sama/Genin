import {Modal} from "antd";
import React from "react";

function CreateZoneModal({open, onClose}: { open: boolean, onClose: () => void }) {

    return (
        <Modal
            title="Create zone"
            open={open}
            onCancel={onClose}
            okText="Create"
        >
            <p>Some contents...</p>
            <p>Some contents...</p>
            <p>Some contents...</p>
        </Modal>
    )
}

export default CreateZoneModal;