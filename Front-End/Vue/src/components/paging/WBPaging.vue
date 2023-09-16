<template>
    <div class="table__paging">
        <span v-html="`${$t('common.total')} <b>${totalRecord}</b> ${$t('common.record')}`"> </span>
        <div class="paging-control flex">
            <p class="paging-title">{{ this.$t("common.totalPaging") }}</p>
            <div class="select-record flex">
                <dropdown-list :menuSelect="recordsPaging" :defaultValue="10"
                    @updateVauleDropdownList="updateVauleDropdownList"></dropdown-list>
            </div>
            <div class="paging">
                <p v-html="`<b>${recordStart} - ${recordEnd}</b> ${$t('common.record')}`"></p>
                <div class="paging-toogle">
                    <div ref="prevPage" class="icon icon-prev" :class="{ 'no-active': !isPrevPageAbel }"
                        @click="clickPrePage"></div>
                    <div ref="afterPage" class="icon icon-after" :class="{ 'no-active': !isNextPageAbel }"
                        @click="clickAfterPage"></div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "wb-paging",
    data() {
        return {
            recordsPaging: [{ id: "1", name: "10" },
            { id: "2", name: "20" },
            { id: "3", name: "50" },
            { id: "4", name: "100" }],
            totalRecord: 20,
            recordEnd: 10,
            recordStart: 1,
            isNextPageAbel: true,
            isPrevPageAbel: true

        }
    },
    props: {
        totalRecord: {
            type: Number,
        },
        recordStartDefault: {
            type: Number,
            default: 1
        },
        recordEndDefault: {
            type: Number,
            default: 10
        }
    },
    methods: {
        /**
        * @description hàm thực hiện kiểm tra có được phép cộng trang tiếp không
        */
        checkAfterPageAbel() {

            if (this.recordEnd >= this.totalRecord) {
                this.isNextPageAbel = false;
            } else {
                this.isNextPageAbel = true
            }
        }, /**
            * @description hàm thực hiện kiểm tra có được phép lùi trang tiếp không
            */
        checkPrevPageAbel() {
            if (this.pageCurrent == 1) {
                this.isPrevPageAbel = false
            } else {
                this.isPrevPageAbel = true
            }
        },
    }
}
</script>
<style scoped>
@import url('./paging.css');
</style>