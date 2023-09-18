<template>
    <div class="table__paging">
        <span v-html="`${$t('common.total')} <b>${totalRecord}</b> ${$t('common.record')}`"> </span>
        <div class="paging-control flex">
            <p class="paging-title">{{ this.$t("common.totalPaging") }}</p>
            <div class="select-record flex">
                <dropdown-list :menuSelect="recordsPaging" :defaultValue="pageSize"
                    @updateVauleDropdownList="updateValuePageSize($event)"></dropdown-list>
            </div>
            <div class="paging">
                <p v-html="`<b>${recordStart} - ${recordEnd}</b> ${$t('common.record')}`"></p>
                <div class="paging-toogle">
                    <wb-icon :iconName="isPrevPageAbel? 'icon-prev' : 'icon-prev no-active'" @handllerClickIcon="clickPrePage"></wb-icon> 
                    <wb-icon :iconName="isNextPageAbel? 'icon-after' :'icon-after no-active'" @handllerClickIcon="clickAfterPage"></wb-icon>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "wb-paging",
    mounted(){
        this.checkAfterPageAbel()
        this.checkPrevPageAbel()
    },
    data() {
        return {
            recordsPaging: [{ id: "1", name: "10" },
            { id: "2", name: "20" },
            { id: "3", name: "50" },
            { id: "4", name: "100" }],
            isNextPageAbel: true,
            isPrevPageAbel: true
        }
    },
    props: {
        totalRecord:{
            type: Number,
        },
        pageCurrent:{
            type:Number,
            default:1
        },
        pageSize:{
            type:Number,
            default:10
        },
        numberOfRecords:{
            type:Number
        }

    },
    computed:{
        // tính toán vị trí bản ghi đầu tiên
        recordStart(){
            if(this.pageCurrent == 1) return 1
            else return (this.pageCurrent - 1 ) * this.pageSize + 1
        },
        // tính toán vị trí bản ghi kết thúc
        recordEnd(){
            if( this.numberOfRecords == this.pageSize ) return this.pageCurrent * this.pageSize
            else return (this.pageCurrent -1 ) * this.pageSize + this.numberOfRecords
        }
    },
    watch:{
        pageSize(newValue) {
            this.checkAfterPageAbel()
            this.checkPrevPageAbel()
        },
        numberOfRecords(newValue){
            this.checkAfterPageAbel()
            this.checkPrevPageAbel()
            console.log("co vao khong")
        },
        pageCurrent(newValue) {
            this.checkAfterPageAbel()
            this.checkPrevPageAbel()
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
         /**
         * @description click vào icon prev sẽ trở về trang trước
         */
         clickPrePage() {
            this.checkPrevPageAbel();
            if (this.isPrevPageAbel) {
                this.$emit('clickPrePage')
                this.isNextPageAbel = true
                this.checkPrevPageAbel()
            }

        },
        // click vào icon after page thì sẽ đến trang tiếp theo
        clickAfterPage() {
            this.checkAfterPageAbel()
            if (this.isNextPageAbel) {
                this.$emit('clickAfterPage')
                this.isPrevPageAbel = true
                if ((this.pageCurrent) * this.pageSize >= this.totalRecord) {
                    this.isNextPageAbel = false
                }
            }
        },
        updateValuePageSize(newPageSize){
            this.$emit('updatePageSize',newPageSize)
        }
    }
}
</script>
<style scoped>
@import url('./paging.css');
</style>