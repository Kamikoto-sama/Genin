import React, {useState} from 'react';
import {Col, ConfigProvider, Layout, Row, Space, theme} from 'antd';
import 'antd/dist/reset.css';
import ConfigsTable from "./components/ConfigsTable";
import ZoneSelect from "./components/ZoneSelect";
import CreateZoneModal from "./components/CreateZoneModal";
import ConfigRoute from "./components/ConfigsRoute";

const App = () => {
    const [createZoneOpen, setCreateZoneOpen] = useState(false);

    return (
        <ConfigProvider theme={{algorithm: theme.darkAlgorithm}}>
            <CreateZoneModal open={createZoneOpen} onClose={() => setCreateZoneOpen(false)}/>
            <Layout className="bg-none">
                <Space size="large" direction="vertical">
                    <Layout.Header>
                        <ZoneSelect onCreateZone={() => setCreateZoneOpen(true)}/>
                    </Layout.Header>
                    <Layout.Content>
                        <Row>
                            <Col offset={4} span={16}>
                                <Space direction="vertical" size="large" style={{width: "100%"}}>
                                    <ConfigRoute/>
                                    <ConfigsTable/>
                                </Space>
                            </Col>
                        </Row>
                    </Layout.Content>
                </Space>
            </Layout>
        </ConfigProvider>
    )
};

export default App;